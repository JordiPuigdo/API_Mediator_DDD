using Application.Users;
using Domain.Abstractions.Messaging;
using log4net;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Application.Abstractions.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheable
    {
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;
        private readonly IDistributedCache _cache;
        
        public CachingBehavior(ILogger<CachingBehavior<TRequest, TResponse>> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            TResponse response;
            if (request.BypassCache) return await next();
            async Task<TResponse> GetResponseAndAddToCache()
            {
                response = await next();
                if (response != null)
                {
                    var slidingExpiration = request.SlidingExpirationInMinutes == 0 ? 30 : request.SlidingExpirationInMinutes;
                    var absoluteExpiration = request.AbsoluteExpirationInMinutes == 0 ? 60 : request.AbsoluteExpirationInMinutes;
                    var options = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpiration))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteExpiration));

                    var serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(response));
                    await _cache.SetAsync(request.CacheKey, serializedData, options, cancellationToken);
                }
                return response;
            }
            var cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);
            if (cachedResponse != null)
            {
                response = JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse))!;
                _logger.LogInformation("fetched from cache with key : {CacheKey}", request.CacheKey);
                _cache.Refresh(request.CacheKey);
            }
            else
            {
                response = await GetResponseAndAddToCache();
                _logger.LogInformation("added to cache with key : {CacheKey}", request.CacheKey);
            }
            return response;
        }

        public async Task RemoveCacheEntry(string cacheKey, CancellationToken cancellationToken)
        {
            await _cache.RemoveAsync(cacheKey, cancellationToken);
            _logger.LogInformation("Removed cache with key: {CacheKey}", cacheKey);
        }
    }
}
