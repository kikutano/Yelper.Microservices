using Identity.API;
using Identity.Application;
using Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider(); //correggere
var configuration = provider.GetService<IConfiguration>();

// Add services to the container.

builder.Services
    .AddApplication(configuration!)
    .AddInfrastructure(configuration!)
    .AddPresentation(configuration!);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
