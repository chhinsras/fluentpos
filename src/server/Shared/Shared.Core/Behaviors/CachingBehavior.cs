using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Settings;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Core.Behaviors
{
    public class CachingBehavior
    {
        // for localization
    }

    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheable
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;
        private readonly IStringLocalizer<CachingBehavior> _localizer;
        private readonly CacheSettings _settings;

        public CachingBehavior(IDistributedCache cache, ILogger<TResponse> logger, IOptions<CacheSettings> settings, IStringLocalizer<CachingBehavior> localizer)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localizer = localizer;
            _settings = settings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;
            if (request.BypassCache)
            {
                _logger.LogInformation(string.Format(_localizer["Bypassing Cache for -> '{0}'."], request.CacheKey));
                return await next();
            }
            async Task<TResponse> GetResponseAndAddToCache()
            {
                response = await next();
                var slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromHours(_settings.SlidingExpiration);
                var options = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration };
                var serializedData = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response));
                await _cache.SetAsync((string)request.CacheKey, serializedData, options, cancellationToken);
                return response;
            }

            var cachedResponse = await _cache.GetAsync((string)request.CacheKey, cancellationToken);
            if (cachedResponse != null)
            {
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
                _logger.LogInformation(string.Format(_localizer["Fetched from Cache -> '{0}'."], request.CacheKey));
            }
            else
            {
                response = await GetResponseAndAddToCache();
                _logger.LogInformation(string.Format(_localizer["Added to Cache -> '{0}'."], request.CacheKey));
            }

            return response;
        }
    }
}