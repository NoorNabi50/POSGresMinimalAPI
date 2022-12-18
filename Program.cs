using Microsoft.AspNetCore.Mvc;
using POSGresApi.Services;
using POSGresApi.Settings;

var builder = WebApplication.CreateBuilder(args);
ConfigurationProperties.ConnectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddSingleton<SalesService>();
var app = builder.Build();

app.MapGet("/api/sales", ([FromServices] SalesService service) => 
{
    var Sales = service.GetAllSales();
    return Sales is null ? Results.NotFound(new { data = "No Records Found", status = 404 }) : Results.Ok(new {data = Sales, status=200});
});



app.Run();
