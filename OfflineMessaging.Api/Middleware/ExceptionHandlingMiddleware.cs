using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private IConfiguration _configuration;

        public ExceptionHandlingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _ravenClient = new RavenClient(_configuration.GetSection("SentryLogger").GetSection("LogDsn").Value);
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
            var response = new TaskCompletionSource<HttpResponse>();
            _ravenClient.Capture(new SentryEvent(exception));
            response.SetException(exception);
            response.SetResult(context.Response);
            return response.Task;
        }
    }
}
