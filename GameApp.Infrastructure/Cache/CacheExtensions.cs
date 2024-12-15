using Microsoft.Extensions.DependencyInjection;

namespace GameApp.Infrastructure.Cache
{
    public static class CacheExtensions
    {
        public static void AddInMemoryCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, MemoryCacheService>();
        }
    }
}
