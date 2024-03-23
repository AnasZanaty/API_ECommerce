using Core;
using Demo.Helper;
using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Reposatiries;
using Microsoft.EntityFrameworkCore;
using Services.ProductServices;
using AutoMapper;
using Services.ProductServices.Dto;
using Demo.MiddleWares;
using Microsoft.AspNetCore.Mvc;
using Demo.HandleResponses;
using Microsoft.Extensions.Configuration;
using Demo.Extensions;
using StackExchange.Redis;
using Core.Identity;
using Core.IdentityEntities;
using Stripe;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace API_Project
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

           
            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            } );
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(Config =>
            {
                var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddLogging();

           

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //before Token UI

            //builder.Services.AddSwaggerGen();

            //After Token UI

            builder.Services.AddSwaggerDocumentation();

            //Cors policy to accept requests fro specific links 

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:3000");
                });
            });

            var app = builder.Build();

            await ApplySeeding.ApplySeedingAsync(app);
           
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseMiddleware<ExceptionMiddleWare>();
            }


            app.UseStaticFiles();



            app.UseCors("CorsPolicy");


            app.UseHttpsRedirection();
            //Authentication first
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();
           


            app.Run();
        }
    }
}