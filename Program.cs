using POSGresApi.Repository;
using POSGresApi.Services;
using POSGresApi.Settings;
using POSGresApi.Extensions;
using POSGresApi.Authentication;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
AppSettings.SetAppSettings(configuration.GetConnectionString("Database"),configuration.GetSection("ApiKeyHeaderName").Value,
    configuration.GetSection("ApiKey").Value
 );


#region Register All nuget package service here

builder.Services.AddEndpointsApiExplorer();
#endregion

#region Register All Services here to resolve Dependency Injection or middlware extension services
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddSingleton<RateLimiterExtension>();


#endregion

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new () { Title = "POSGres Sales Minimal API", Version = "v1" });
});

var app = builder.Build();

#region Register All Middlwares Sequentially on HTTP Request PipleLine  (The order matters Alot Here)
app.AuthenticateRequest();
app.UseMiddleware<RateLimiterExtension>();
app.ConfigureExceptionHanler();
app.ConfigureEndPoints();

#endregion 

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "";
});
app.Run();
