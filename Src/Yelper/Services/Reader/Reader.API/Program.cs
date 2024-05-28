using Reader.API;
using Reader.API.EndPoints;
using Reader.API.IntegrationEvents;
using Reader.API.IntegrationEvents.Receiver.Yelps;
using Reader.Application;
using Reader.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider(); //correggere
var configuration = provider.GetService<IConfiguration>();

// Add services to the container.
builder.Services.AddScoped<NewYelpIntegrationEventHandler>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication(configuration!)
    .AddInfrastructure(configuration!)
    .AddPresentation(configuration!);

var app = builder.Build();

app.MapTrendsEndpoints();

EventBusSubscriber.SubscribeAllEventBus(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program;
