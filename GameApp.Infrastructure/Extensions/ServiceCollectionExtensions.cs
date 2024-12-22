using GameApp.Infrastructure.Hashing;
using GameApp.Infrastructure.Repositories.Abstracts;
using GameApp.Infrastructure.Repositories.Concretes;
using GameApp.Infrastructure.Repositories.Context;
using GameApp.Infrastructure.Service.Abstracts;
using GameApp.Infrastructure.Service.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLogCollection(this IServiceCollection services, string dbConettion)
        {
            services.AddDbContext<LogDBContext>(options =>
            {
                options.UseSqlServer(dbConettion);
            });

            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogService, LogService>();

            return services;
        }
        public static IServiceCollection AddHasherCollection(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
