using Microsoft.AspNetCore.Mvc;
using POSGresApi.Abstraction;
using POSGresApi.Repository;

namespace POSGresApi.EndPoints
{
    public class SalesEndPoint : IRegisterEndPoints
    {
        public void RegisterEndPoints(WebApplication app)
        {
            app.MapGet("/api/sales/", async ([FromServices] ISalesService service) =>
            {
                var response = await service.GetAllSales();
                return response is null ? Results.Ok(new { data = "No Records Found", status = 404 }) : Results.Ok(new { data = response, status = 200 });
            });

            app.MapGet("/api/sales/{id}", async ([FromServices] ISalesService service, int id) =>
            {
                var response = await service.GetSalesById(id);
                return response is null ? Results.Ok(new { data = "No Records Found", status = 404 }) : Results.Ok(new { data = response, status = 200 });
            });

            app.MapGet("/api/sales/salesDetail/{id}", async ([FromServices] ISalesService service, int id) =>
            {
                var response = await service.GetSalesDetailById(id);
                return response is null ? Results.Ok(new { data = "No Records Found", status = 404 }) : Results.Ok(new { data = response, status = 200 });
            });
        }
    }
}
