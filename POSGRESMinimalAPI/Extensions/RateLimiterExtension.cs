using System.Collections.Concurrent;

namespace POSGresApi.Extensions
{
    public class RateLimiterExtension : IMiddleware
    {
        private static int maxRequests { get; } = 4;

        private readonly Timer timer; 
        private  ConcurrentDictionary<string,int>? tokenBuckets { get; set; }
        public RateLimiterExtension() {
            tokenBuckets = new ConcurrentDictionary<string,int>();
            timer = new Timer((Object agr) => tokenBuckets.Clear(), null, TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(3));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (executeRateLimiter(context))
                await next(context).ConfigureAwait(false);
            else
            {
              await context.Response.WriteAsJsonAsync(new { data = "Sorry, you have exceeded the maximum number of requests allowed. Please wait and try again later, or contact customer support for assistance", 
                    status = StatusCodes.Status429TooManyRequests }).ConfigureAwait(false);
            }

        }
        private bool executeRateLimiter(HttpContext context)
        {
            string requestConnectionId = "Noor50" ;
            tokenBuckets.AddOrUpdate(requestConnectionId, 1, (key, value) => value + 1);
            return tokenBuckets[requestConnectionId] > maxRequests ? false : true;
        }

    }
}
