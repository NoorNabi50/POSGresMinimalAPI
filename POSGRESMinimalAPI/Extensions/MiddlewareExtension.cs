namespace POSGresApi.Settings
{
    public static class MiddlewareExtension
    {

        public static void ConfigureMiddleWare(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                    if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                    {
                        if (isValidURL(context))
                            await context.Response.WriteAsJsonAsync(new { data = "No resoruce available on the requested url", status = StatusCodes.Status404NotFound });
                    }
                }
                catch (Exception e)
                {
                    await context.Response.WriteAsJsonAsync(new { data = "The request is not in correct format", status = StatusCodes.Status400BadRequest });
                }
            });

        }
        private static List<String> getValidRequestURLs()
        {
            List<String> urls = new List<string>()
            {
                "/api/sales/",
                "/api/sales/salesDetail/"
            };
            return urls;
        }

        private static bool isValidURL(this HttpContext httpContext)
        {
            String requestUrl = httpContext.Request.Path;
            String isValid = getValidRequestURLs().FirstOrDefault(x => x.Contains(requestUrl));
            return isValid != null;
        }
    }
}
