global using ILogger = Serilog.ILogger; 
using WebApplication7.Endpoints;
using WebApplication7.Middleware;
using XitMent.Core.Serilog;
using XitMent.Core.Serilog.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddTransient<ExceptionMiddleware>();

builder.UseXitMentSerilog(new SerilogSetting(
    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT").ToDeployEnvironment(),
    builder.Configuration["Project"]!,
    Environment.GetEnvironmentVariable("POD_NAME"),
    Environment.GetEnvironmentVariable("NODE_NAME"),
    Environment.GetEnvironmentVariable("ELK_USERNAME"),
    Environment.GetEnvironmentVariable("ELK_PASSWARD")
));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapTodosApi();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();