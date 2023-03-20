namespace POSGresApi.Settings
{
    public static class AppSettings
    {
        public static string? ConnectionString { get; private set; }
        public static string? ApiKeyHeaderName { get;  private set; }
        public static string? ApiKey { get; private set; }

        public static void SetAppSettings(string? connectionString, string? apiKeyHeaderName, string? apiKey)
        {
            ConnectionString = connectionString;
            ApiKeyHeaderName = apiKeyHeaderName;
            ApiKey = apiKey;
            Console.WriteLine("Logger: App Settings Have been changed DateTime " + DateTime.Now.ToString());
        }


    }
}
