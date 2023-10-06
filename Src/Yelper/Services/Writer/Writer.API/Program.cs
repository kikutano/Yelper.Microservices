using EventBus.Interfaces;
using Writer.API;
using Writer.API.IntegrationEvents.Receiver.Users;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider(); //correggere
var configuration = provider.GetService<IConfiguration>();

// Add services to the container.

builder.Services
	.AddPresentation(configuration!);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO: Incapsulate!
builder.Services.AddScoped<UserCreatedIntegrationEventHandler>();

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
app.Services
	.GetService<IEventBusSubscriptionsManager>()!
	.SetServiceScope(app.Services.CreateScope());

//TODO: Incapsulate!
eventBus.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();

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

public partial class Program { }