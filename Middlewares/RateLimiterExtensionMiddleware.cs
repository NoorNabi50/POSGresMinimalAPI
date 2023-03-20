using System.Collections.Concurrent;

namespace POSGresApi.Extensions
{
    public class RateLimiterExtensionMiddleware : IMiddleware
    {
        private ConcurrentDictionary<string, int> tokensBucket { get; set; }

        private const int maxRequests = 5 ;
        private Timer? timer { get; set; }

        public RateLimiterExtensionMiddleware()
        {
            tokensBucket = new ConcurrentDictionary<string, int>();
            timer = new Timer(clearTokenBucket, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate _next)
        {

            if (isRequestNotAllowed(context))
            {
                context.Response.WriteAsJsonAsync(new { date = "The no of requests have been exceeded , Plz try Again Later", status = StatusCodes.Status429TooManyRequests });
                return Task.CompletedTask;
            }
            return _next(context);
        }

        private bool isRequestNotAllowed(HttpContext context)
        {
            string requestIpKey = context.Connection.RemoteIpAddress.ToString();
            tokensBucket.AddOrUpdate(requestIpKey, 1, (key, value) => value + 1);
            return tokensBucket[requestIpKey] > maxRequests;
        }

        private void clearTokenBucket(object obj)
        {
            Console.WriteLine("Debug: ***********************************Going to clear Token Bucket************************************");
            tokensBucket.Clear();
        }
    }

}
