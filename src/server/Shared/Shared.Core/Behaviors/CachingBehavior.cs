using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Settings;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Exceptions;

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
        private readonly IJsonSerializer _jsonSerializer;
        private readonly CacheSettings _settings;

        public CachingBehavior(IDistributedCache cache, ILogger<TResponse> logger, IOptions<CacheSettings> settings, IStringLocalizer<CachingBehavior> localizer, IJsonSerializer jsonSerializer)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localizer = localizer;
            _jsonSerializer = jsonSerializer;
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
                if (slidingExpiration <= TimeSpan.Zero)
                {
                    throw new CustomException(_localizer["Cache Sliding Expiration must be greater than 0."], statusCode: HttpStatusCode.BadRequest);
                }
                var options = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration };
                var serializedData = Encoding.Default.GetBytes(_jsonSerializer.Serialize(response));
                await _cache.SetAsync((string)request.CacheKey, serializedData, options, cancellationToken);
                return response;
            }

            var cachedResponse = !string.IsNullOrWhiteSpace(request.CacheKey) ? await _cache.GetAsync(request.CacheKey, cancellationToken) : null;
            if (cachedResponse != null)
            {
                response = _jsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse));
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