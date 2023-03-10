using BookPlatform.AIAPI.Services;
using BookPlatform.BLL.Intefaces;
using BookPlatform.BLL.Services;
using BookPlatform.MQ.Interfaces;
using BookPlatform.MQ.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookPlatform.BLL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddAutoMapper(typeof(AppMappingProfile));
            services.AddSingleton<IMessageBrokerService, RabbitMQService>();

            return services;
        }
    }
}
