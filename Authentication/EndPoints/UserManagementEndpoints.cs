using Microsoft.AspNetCore.Mvc;
using POSGresApi.Abstraction;
using POSGresApi.Authentication.Abstraction;
using POSGresApi.CommonModels;

namespace POSGresApi.Authentication.EndPoints
{
    public class UserManagementEndpoints : IRegisterEndPoints
    {
        public void RegisterEndPoints(WebApplication app)
        {

            app.MapGet("/api/users/getAllUsers/", async ([FromServices] IUserManagementService service) =>
            {
                var response = await service.GetAllUsersBySP();
                return response is null ? Results.Ok(new { data = "No Records Found", status = StatusCodes.Status204NoContent }) :
                                   Results.Ok(new { data = response, status = StatusCodes.Status200OK });
            });
            app.MapPost("/api/users/saveUser/", async ([FromServices] IUserManagementService service, [FromBody] RequestPayLoad requestModel) =>
            {
                //   string payload = "[{\"name\": \"BULAA\", \"email\": \"bulla@example.com\",\"phone\": \"28821234\"}]";
                var response = await service.SaveUserBySP(requestModel.payload);
                return response is null ? Results.Ok(new { message = "Data not saved successfully", status = StatusCodes.Status500InternalServerError }) :
                                   Results.Ok(new { data = response, status = StatusCodes.Status201Created });
            });


        }
    }
}
