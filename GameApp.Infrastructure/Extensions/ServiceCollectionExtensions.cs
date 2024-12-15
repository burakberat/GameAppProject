using Castle.DynamicProxy;
using GameApp.Infrastructure.Interceptors;
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

        public static IServiceCollection AddProxiedServices(this IServiceCollection services, ProxyGenerator proxyGenerator)
        {
            var list = services.Where(x => x.Lifetime == ServiceLifetime.Scoped && x.ServiceType.Name.EndsWith("Service") && !x.ServiceType.FullName.StartsWith("Infra.") && !x.ServiceType.FullName.StartsWith("Microsoft")).ToList();

            foreach (var item in list)
            {
                var implementationType = item.ImplementationType;
                services.Remove(item);

                services.AddScoped(serviceProvider =>
                {
                    var target = ActivatorUtilities.CreateInstance(serviceProvider, implementationType);
                    var interceptor = serviceProvider.GetRequiredService<CachingInterceptor>();
                    return proxyGenerator.CreateClassProxyWithTarget(implementationType, target, interceptor);

                });
            }
            return services;
        }
    }
}
