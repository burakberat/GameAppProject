﻿using Castle.DynamicProxy;
using GameApp.Infrastructure.Attributes;
using GameApp.Infrastructure.Cache;
using System.Reflection;
using System.Text.Json;

namespace GameApp.Infrastructure.Interceptors
{
    public class CachingInterceptor : IInterceptor
    {
        private readonly ICacheService _cacheService;
        public CachingInterceptor(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async void Intercept(IInvocation invocation)
        {
            var cacheAttribute = invocation.Method.GetCustomAttribute<CacheAttribute>();

            if (cacheAttribute != null)
            {
                string key = cacheAttribute.Key;
                int durationSeconds = cacheAttribute.DurationSeconds;

                var cacheData = await _cacheService.GetStringAsync(key);

                if (cacheData != null)
                {
                    invocation.ReturnValue = JsonSerializer.Deserialize(cacheData, invocation.Method.ReturnType);
                    return;
                }

                invocation.Proceed();
                var result = invocation.ReturnValue;

                if (result != null)
                {
                    var data = JsonSerializer.Serialize(result, invocation.Method.ReturnType);
                    await _cacheService.SetStringAsync(key, data, durationSeconds);
                }
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
