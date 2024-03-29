﻿namespace POSGresApi.Extensions
{
    public static class ExceptionHandlingMiddleware
    {

        public static void ConfigureExceptionHandlingMiddlware(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                    if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                    {
                        if (context.isValidURL())
                            await context.Response.WriteAsJsonAsync(new { data = "No resoruce available on the requested url", status = StatusCodes.Status404NotFound });
                    }
                }
                catch (Exception e)
                {
                    await context.Response.WriteAsJsonAsync(new { data = "Something went wrong while processing the request!", status = StatusCodes.Status500InternalServerError });
                }
            });

        }
        private static List<string> getValidRequestURLs()
        {
            List<string> urls = new List<string>()
            {
                "/api/sales/",
                "/api/sales/salesDetail/"
            };
            return urls;
        }

        private static bool isValidURL(this HttpContext httpContext)
        {
            string requestUrl = httpContext.Request.Path;
            string isValid = getValidRequestURLs().FirstOrDefault(x => x.Contains(requestUrl));
            return isValid != null;
        }
    }
}
