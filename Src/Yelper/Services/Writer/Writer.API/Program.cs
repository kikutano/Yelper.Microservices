using Writer.API;
using Writer.API.IntegrationEvents;
using Writer.Application;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider(); //correggere
var configuration = provider.GetService<IConfiguration>();

builder.Services
	.AddApplication(configuration!)
	.AddPresentation(configuration!);

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

public partial class Program { }