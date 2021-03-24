﻿using Microsoft.Extensions.DependencyInjection;
using MyAPI.Business.Interfaces;
using MyAPI.Data.Context;
using MyAPI.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ApplicationContext>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();


            return services;
        }
    }
}
