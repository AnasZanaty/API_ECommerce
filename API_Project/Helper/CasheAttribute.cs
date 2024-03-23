using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.CasheServices;
using System.Text;

namespace Demo.Helper
{
    public class CacheAttribute :Attribute , IAsyncActionFilter

    {
        private readonly int timeToLiveInSec;

        public CacheAttribute(int TimeToLiveInSec)
        {
            timeToLiveInSec = TimeToLiveInSec;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context , ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICasheServices>();
            var cacheKey= GeneratedCacheKeyFromRequest(context.HttpContext.Request);
            var CachResponse = await cacheService.GetCacheResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(CachResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = CachResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result= contentResult;

                return;
            }
            else
            {
                var executedContext = await next();
                if(executedContext.Result is OkObjectResult response)
                {
                    await cacheService.setCaheResponseAsync(cacheKey, response.Value, TimeSpan.FromSeconds(timeToLiveInSec));
                }
            }
        }

        private string GeneratedCacheKeyFromRequest (HttpRequest Request) 
        {
            var cacheKey = new StringBuilder();

            cacheKey.Append(Request.Path);

            foreach(var (key , value) in Request.Query.OrderBy(x=>x.Key)) //tuple
                
            {

                cacheKey.Append(key+"-"+value);
            
            }

            return cacheKey.ToString();



        }
    }
}
