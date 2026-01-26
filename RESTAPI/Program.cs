using RESTAPI.Services;
using RESTAPI.Clients;
using RESTAPI.Services.Interfaces;
using RESTAPI.Clients.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient<IGeocoderApiClient, GeocoderApiClient>(client =>
{
    client.BaseAddress = new Uri("http://api.openweathermap.org/geo/1.0/");
    client.Timeout = TimeSpan.FromSeconds(10);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IOpenWeatherApiClient, OpenWeatherApiClient>(client =>
{
    client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
    client.Timeout = TimeSpan.FromSeconds(10);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped<IOpenWeatherService, OpenWeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
