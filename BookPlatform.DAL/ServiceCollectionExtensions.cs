using BookPlatform.DAL.Intefaces;
using BookPlatform.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookPlatform.DAL
{
    public static class ServiceCollectionExtensions
    {
        private static IServiceCollection AddDbContext(this IServiceCollection services, string sqlConnectionString)
        {
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlite(sqlConnectionString));

            return services;
        }

        public static IServiceCollection AddEfRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext(connectionString);
            services.AddScoped<IBookRepository, BookRepository>();

            return services;
        }
    }
}
