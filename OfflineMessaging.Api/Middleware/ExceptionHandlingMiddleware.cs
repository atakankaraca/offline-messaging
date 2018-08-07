using Microsoft.AspNetCore.Http;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Threading.Tasks;

namespace OfflineMessaging.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private RavenClient _ravenClient;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
            _ravenClient = new RavenClient("https://48da261058fe49daab3568aea03c839a@sentry.io/1256036");
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _ravenClient.Capture(new SentryEvent(exception));
            return Task.CompletedTask;
        }
    }
}
