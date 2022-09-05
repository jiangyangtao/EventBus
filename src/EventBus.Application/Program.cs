using EventBus.Core;
using EventBus.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


var services = builder.Services;
var configuration = builder.Configuration;

services.Configure<ApiBehaviorOptions>(options =>
{
    // 模型校验失败处理
    options.InvalidModelStateResponseFactory = (context) =>
    {
        var errorMessage = context.ModelState.GetValidationSummary();
        return new JsonResult(new
        {
            Success = false,
            Status = 400,
            Msg = errorMessage,
            Data = string.Empty,
        });
    };
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddEventBus(configuration);


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseEventBus();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
