using BookPlatform.AIAPI.Interfaces;
using BookPlatform.AIAPI.Services;
using BookPlatform.MQ.Interfaces;
using BookPlatform.MQ.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPlatform.AIAPI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAIServices(this IServiceCollection services)
        {
            services.AddSingleton<IAIService, AIService>();
            services.AddSingleton<IAIAPIService, AIAPIService>();
            services.AddSingleton<IMessageBrokerService, RabbitMQService>();

            // Add RabbitMQ Listener
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var rabbitMQService = provider.GetService<IMessageBrokerService>();
                return new RabbitMQListenerAI(rabbitMQService, "my-routing-key",
                    configuration.GetSection("MQsettings:IncomingAIQueueName").Value,
                    configuration, provider.GetService<IAIService>());
            });

            return services;
        }

    }

}
