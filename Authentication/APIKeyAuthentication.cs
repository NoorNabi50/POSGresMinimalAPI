using Microsoft.Extensions.Caching.Memory;
using POSGresApi.Settings;
using System.Security.Cryptography;

namespace POSGresApi.Authentication
{
    public class APIKeyAuthentication
    {

        public static Task<string> createAuthenticationKey(int userId,IMemoryCache cache)
        {
            string APIKeyValue = string.Concat(AppSettings.ApiKey,"_", userId);
            cache.Set(userId, APIKeyValue, new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(3)
            }); 
            return Task.FromResult(APIKeyValue);
        }
        public static bool isValidAPIKey(string? apiKey, IMemoryCache cache)
        {

            if (string.IsNullOrEmpty(apiKey)) return false;

            int cacheKey = int.Parse(apiKey.Split("_")[1]);
            if(cache.TryGetValue(cacheKey, out string entry))
            {
                Console.WriteLine(entry);

                return entry.Equals(apiKey);
            }
            return false;
        }
    }
}
