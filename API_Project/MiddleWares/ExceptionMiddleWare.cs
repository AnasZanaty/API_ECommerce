using Demo.HandleResponses;
using System.Net;
using System.Text.Json;

namespace Demo.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> logger;
        private readonly IHostEnvironment environment;

        //middleware takes request and environment 
        public ExceptionMiddleWare(RequestDelegate next , ILogger<ExceptionMiddleWare>logger , IHostEnvironment environment)
        {
            _next = next;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task InvokeAsync (HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e) 
            {
                logger.LogError(e,e.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode=(int)HttpStatusCode.InternalServerError; //500

                //cant return error except in development environment
                var response = environment.IsDevelopment()
                    //If It's a Development Environment
                   ? new ApiException((int)HttpStatusCode.InternalServerError, e.Message, e.StackTrace.ToString())
                //If it's not Development Environment
                   : new ApiException((int)HttpStatusCode.InternalServerError);


                //Not Nesseccary

                var options = new JsonSerializerOptions { PropertyNamingPolicy= JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            
            }

        }
    }
}
