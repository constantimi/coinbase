using System.Net;
using System.Text.Json;

namespace Coinbase.Services.Identity.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    AppException e => (int) HttpStatusCode.BadRequest,// custom application error
                    KeyNotFoundException e => (int) HttpStatusCode.NotFound,// not found error
                    _ => (int) HttpStatusCode.InternalServerError,// unhandled error
                };

                string result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
