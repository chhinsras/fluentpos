// --------------------------------------------------------------------------------------------------
// <copyright file="TwilioSmsService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.DTOs.Sms;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace FluentPOS.Shared.Infrastructure.Services
{
    public class TwilioSmsService : ISmsService
    {
        private readonly SmsSettings _settings;
        private readonly ILogger<TwilioSmsService> _logger;

        public TwilioSmsService(IOptions<SmsSettings> settings, ILogger<TwilioSmsService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public Task SendAsync(SmsRequest request)
        {
            try
            {
                string accountSid = _settings.SmsAccountIdentification;
                string authToken = _settings.SmsAccountPassword;

                TwilioClient.Init(accountSid, authToken);

                return MessageResource.CreateAsync(
                    to: new PhoneNumber(request.Number),
                    from: new PhoneNumber(_settings.SmsAccountFrom),
                    body: request.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return Task.CompletedTask;
        }
    }
}