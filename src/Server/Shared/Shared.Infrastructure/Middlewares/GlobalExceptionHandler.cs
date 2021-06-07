using FluentPOS.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Infrastructure.Middlewares
{
    internal class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var statusCode = 500;
                var code = "error";
                var message = "There was an error.";
                _logger.LogError(exception, exception.Message);
                if (exception is CustomException customException)
                {
                    statusCode = 400;
                    code = customException.GetType().Name.Replace("_"," ");
                    message = customException.Message;
                }
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(new { code, message });
            }
        }
    }
}
