using Microsoft.OpenApi.Models;
namespace Demo.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiDeom", Version = "v1" });
                var SecurityScheme = new OpenApiSecurityScheme
                {
                    Description = "Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In= ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }
                };
                c.AddSecurityDefinition("bearer", SecurityScheme);

                var SecurityRequirements = new OpenApiSecurityRequirement
                {
                    {SecurityScheme, new []{"bearer"} }
                };

                c.AddSecurityRequirement(SecurityRequirements);
            });
            return services;
        }
    }
}
