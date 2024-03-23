using Infrastructure.Interfaces;
using Infrastructure.Reposatiries;
using Microsoft.AspNetCore.Mvc;
using Services.ProductServices.Dto;
using Services.ProductServices;
using Demo.HandleResponses;
using Services.CasheServices;
using Services.BasketServices.Dto;
using Services.BasketServices;
using Infrastructure.BasketReposatory;
using Services.TokenService;
using Services.UserServices;
using Services.OrderServices.Dto;
using Services.OrderServices;
using Services.PaymentServices;

namespace Demo.Extensions
{
    public static class ApplicationServicesExtensions 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductReposatory, ProductReposatory>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericReposatory<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICasheServices, CasheService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketReposatory, BasketReposatory>();
            services.AddScoped<ITokenService, TokenServices>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IorderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentServices>();


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errors = ActionContext.ModelState.Where(model => model.Value.Errors.Count > 0)
                                                         .SelectMany(model => model.Value.Errors)
                                                         .Select(error => error.ErrorMessage).ToList();
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });


            // builder.Services.AddAutoMapper(X => X.AddProfile(new ProductProfile()))      =====>>>>    // ProductUrl لسبب لا اعلمه مش هتعرف تعمل اوبجكت من ال ;



            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));
            services.AddAutoMapper(typeof(OrderProfile));


            return services;
        }
    }
}
