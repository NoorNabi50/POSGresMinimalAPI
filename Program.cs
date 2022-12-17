using POSGresApi.Settings;

var builder = WebApplication.CreateBuilder(args);
ConfigurationProperties.ConnectionString = builder.Configuration.GetConnectionString("Database");
var app = builder.Build();

app.MapGet("/", () => 
{

    return "Hello World!";

});



app.Run();
