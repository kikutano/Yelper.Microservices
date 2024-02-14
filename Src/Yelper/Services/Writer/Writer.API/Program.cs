using Writer.API;
using Writer.API.IntegrationEvents;
using Writer.API.IntegrationEvents.Receiver.Users;
using Writer.Application;
using Writer.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider(); //correggere
var configuration = provider.GetService<IConfiguration>();

builder.Services
    .AddApplication(configuration!)
    .AddInfrastructure(configuration!)
    .AddPresentation(configuration!);

builder.Services.AddScoped<UserCreatedIntegrationEventHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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