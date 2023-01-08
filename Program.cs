using Microsoft.AspNetCore.Mvc;
using POSGresApi.EndPoints;
using POSGresApi.Repository;
using POSGresApi.Services;
using POSGresApi.Settings;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
ConfigurationProperties.ConnectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddSingleton<ISalesService, SalesService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new () { Title = "POSGres Sales Minimal API", Version = "v1" });
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    await next();

    if(context.Response.StatusCode == StatusCodes.Status404NotFound)
    {
        if (!context.isValidURL())
        {
            context.Response.ContentType = Text.Plain;
            await context.Response.WriteAsync("No resoruce available on the requested url");
        }
    }
});


app.ConfigureEndPoints();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "";
});
app.Run();
