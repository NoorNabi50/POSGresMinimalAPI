namespace POSGresApi.Settings
{
    public static class RequestURLs
    {

        private static List<String> getValidRequestURLs()
        {
            List<String> urls = new List<string>()
            {
                "/api/sales/",
                "/api/sales/salesDetail/"        
            };

            return urls;
        }

        public static bool isValidURL(this HttpContext httpContext)
        {
            String requestUrl = httpContext.Request.Path;
            String isValid =  getValidRequestURLs().FirstOrDefault(x=>x.Contains(requestUrl));
            return isValid != null;
        }
    }
}
