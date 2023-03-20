using Microsoft.Extensions.Caching.Memory;
using POSGresApi.Authentication.Services;
using POSGresApi.Settings;
using System.Collections.Concurrent;

namespace POSGresApi.Middlewares
{
    public static class APIKeyAuthenticationMiddleware
    {

        #region Middware to authenticate each request against key
        public static IApplicationBuilder AuthenticateRequest(this WebApplication app)
        {
            return app.UseWhen((context) => !context.Request.Path.StartsWithSegments("/login"), appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    string apiKey = context.Request.Headers[AppSettings.ApiKeyHeaderName];
                    var cache = context.RequestServices.GetService(typeof(IMemoryCache)) as IMemoryCache;
                    if (!APIKeyAuthentication.isValidAPIKey(apiKey, cache))
                    {
                        await context.Response.WriteAsJsonAsync(new { data = "Unvalid Request", status = StatusCodes.Status400BadRequest });
                    }
                    else
                    {
                        await next();

                    }
                });
            });

        }

        #endregion



    }
}
