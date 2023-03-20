using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using POSGresApi.Abstraction;
using POSGresApi.Authentication.Services;

namespace POSGresApi.Authentication.EndPoints
{
    public class AuthenticationEndPoint : IRegisterEndPoints
    {
        public void RegisterEndPoints(WebApplication app)
        {
            app.MapPost("/login/{userId}/{passCode}", async ([FromServices] IMemoryCache cache, int userId, int passCode) =>
            {
                if (passCode == 5050)
                {
                    string apiToken = await APIKeyAuthentication.createAuthenticationKey(userId, cache);

                    return Results.Ok(new
                    {
                        date = "Login SuccessFully",
                        Token = apiToken,
                        status = StatusCodes.Status200OK
                    });
                }
                return Results.Unauthorized();
            });
        }
    }
}
