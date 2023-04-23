using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using POSGresApi.Abstraction;
using POSGresApi.Sales.Abstraction;

namespace POSGresApi.Sales.EndPoints
{
    public class SalesEndPoint : IRegisterEndPoints
    {
        public void RegisterEndPoints(WebApplication app)
        {

            app.MapGet("/api/sales/", async ([FromServices] ISalesService service) =>
            {
                var response = await service.GetAllSalesByTwoDBRequests();
                return response is null ? Results.Ok(new { data = "No Records Found", status = StatusCodes.Status204NoContent }) : Results.Ok(new { data = response, status = StatusCodes.Status200OK });
            });
            app.MapGet("/api/sales/{id}", async ([FromServices] ISalesService service, int id) =>
            {
                var response = await service.GetSalesById(id);
                return response is null ? Results.Ok(new { data = "No Records Found", status = StatusCodes.Status204NoContent }) : Results.Ok(new { data = response, status = StatusCodes.Status200OK });
            });
            app.MapGet("/api/sales/salesDetail/{id}", async ([FromServices] ISalesService service, int id) =>
            {
                var response = await service.GetSalesDetailById(id);
                return response is null ? Results.Ok(new { data = "No Records Found", status = StatusCodes.Status204NoContent }) : Results.Ok(new { data = response, status = StatusCodes.Status200OK });
            });

            /*app.MapGet("/api/tvShows/browseFavoriteCollection/{id}", async (TvShowsService tvShowsService, [FromServices] IMemoryCache cache, int id) =>
            {
                 if(!cache.TryGetValue(id,out object data))
                {
                    data = await tvShowsService.GetFavoriteCollectionById(id);
                    cache.Set(id, data, new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddDays(3)
                    });
                }
                return Results.Ok(new { data = data, status = StatusCodes.Status200OK });

            });

*/
        }
    }
}
