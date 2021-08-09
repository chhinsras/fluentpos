// --------------------------------------------------------------------------------------------------
// <copyright file="CachingBehavior.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Exceptions;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Settings;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FluentPOS.Shared.Core.Behaviors
{
    public class CachingBehavior
    {
        // for localization
    }

    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheable
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

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
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
                byte[] serializedData = Encoding.Default.GetBytes(_jsonSerializer.Serialize(response));
                await _cache.SetAsync(request.CacheKey, serializedData, options, cancellationToken);
                return response;
            }

            byte[] cachedResponse = !string.IsNullOrWhiteSpace(request.CacheKey) ? await _cache.GetAsync(request.CacheKey, cancellationToken) : null;
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