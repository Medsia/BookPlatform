using BookPlatform.AI.Interfaces;
using BookPlatform.AI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPlatform.AI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAIServices(this IServiceCollection services)
        {
            services.AddScoped<IAIService, AIService>();
            services.AddScoped<IAIAPIService, AIAPIService>();

            return services;
        }

    }

}
