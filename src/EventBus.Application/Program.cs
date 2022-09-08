using EventBus.Application.Filters;
using EventBus.Core;
using EventBus.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
}).ConfigureApiBehaviorOptions(options =>
{
    // 模型校验失败处理
    options.InvalidModelStateResponseFactory = (context) =>
    {
        var errorMessage = context.ModelState.GetValidationSummary();
        var resultContent = new { Code = 400, Message = errorMessage, };
        var result = new JsonResult(resultContent)
        {
            StatusCode = 400,
        };
        return result;
    };
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Formatting = Formatting.Indented;
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    //options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
    //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
//services.Configure<ApiBehaviorOptions>(options =>
//{
//    // 模型校验失败处理
//    options.InvalidModelStateResponseFactory = (context) =>
//    {
//        var errorMessage = context.ModelState.GetValidationSummary();
//        var resultContent = new { Code = 400, Message = errorMessage, };
//        var result = new JsonResult(resultContent)
//        {
//            StatusCode = 400,
//        };
//        return result;
//    };
//});

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEventBus();
app.UseRouting();
app.UseHttpsRedirection();
//app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello, EventBus");
});
//app.MapRazorPages();

app.Run();
