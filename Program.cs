using Microsoft.AspNetCore.Mvc;
using POSGresApi.Services;
using POSGresApi.Settings;

var builder = WebApplication.CreateBuilder(args);
ConfigurationProperties.ConnectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddSingleton<SalesService>();
var app = builder.Build();

app.MapGet("/api/sales", ([FromServices] SalesService service) => 
{

    return service.GetAllSales();

});



app.Run();
