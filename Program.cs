using POSGresApi.Settings;
using POSGresApi.Extensions;
using POSGresApi.Sales.Abstraction;
using POSGresApi.Sales.Services;
using POSGresApi.Middlewares;
using POSGresApi.Authentication.Abstraction;
using POSGresApi.Authentication.Services;

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
//builder.Services.AddScoped<ISalesService, SalesService>(provider => new SalesService());
builder.Services.AddScoped<ISalesService, SalesServiceDapper>(provider => new SalesServiceDapper());
builder.Services.AddScoped<IUserManagementService,UserManagementService>();
builder.Services.AddSingleton<RateLimiterExtensionMiddleware>();


#endregion
var app = builder.Build();

#region Register All Middlwares Sequentially on HTTP Request PipleLine  (The order matters Alot Here)
app.AuthenticateRequest();
app.UseMiddleware<RateLimiterExtensionMiddleware>();
app.ConfigureExceptionHandlingMiddlware();
app.ConfigureEndPointsRegisterationMiddlware();

#endregion 
app.Run();
