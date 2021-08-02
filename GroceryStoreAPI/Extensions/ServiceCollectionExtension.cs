using FluentValidation.AspNetCore;
using GroceryStore.Core.Contracts;
using GroceryStore.Core.Repositories;
using GroceryStore.Services;
using GroceryStore.Services.Contracts;
using GroceryStoreAPI.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace GroceryStoreAPI.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDependencyContainer(this IServiceCollection services) 
        {
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddSwaggerConfigurations(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Grocery Store API", Version = "v1" });
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
            });
        }
        public static void AddFilterConfigurations(this IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(ResponseExceptionFilter));
                opt.Filters.Add(typeof(ValidateModelAttribute));
            }).AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>());
        }
    }
}
