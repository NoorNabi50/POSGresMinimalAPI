﻿using System.Collections.Concurrent;

namespace POSGresApi.Extensions
{
    public class RateLimiterExtension : IMiddleware
    {
        private ConcurrentDictionary<string, int>? tokensBucket { get; set; }

        private const int maxRequests = 4;
        private Timer? timer { get; set; }

        public RateLimiterExtension()
        {
            tokensBucket = new ConcurrentDictionary<string, int>();
            timer = new Timer(clearTokenBucket, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (isRequestNotAllowed(context))
            {
                context.Response.WriteAsJsonAsync(new { date = "The no of requests have been exceeded , Plz try Again Later" });
                return Task.CompletedTask;
            }
            return next(context);
        }

        private bool isRequestNotAllowed(HttpContext context)
        {
            string requestIpKey = context.Connection.RemoteIpAddress.ToString();
            tokensBucket.AddOrUpdate(requestIpKey, 1, (key, value) => value + 1);
            return tokensBucket[requestIpKey] > maxRequests;
        }

        private void clearTokenBucket(Object obj)
        {
            Console.WriteLine("Debug: ***********************************Going to clear Token Bucket************************************");
            tokensBucket.Clear();
        } 
    }

}