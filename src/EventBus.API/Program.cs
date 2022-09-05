using EventBus.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddEventBus(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEventBus();
app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello, EventBus");
});

app.Run();
