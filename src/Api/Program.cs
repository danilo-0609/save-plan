using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SavePlan.API;
using SavePlan.API.Infrastructure;
using SavePlan.API.Middlewares;
using Serilog;
using System.Collections;

var builder = WebApplication.CreateBuilder(args);

var jsonConfig = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .Build();

// Get all environment variables
var environmentalVariables = Environment.GetEnvironmentVariables();

// Replace placeholders with environment variables
foreach (var child in jsonConfig.AsEnumerable())
{
    if (!string.IsNullOrEmpty(child.Value) && child.Value.Contains("${"))
    {
        var updatedValue = child.Value;

        foreach (DictionaryEntry env in environmentalVariables)
        {
            string placeholder = $"${{{env.Key}}}";
            updatedValue = updatedValue.Replace(placeholder, env.Value?.ToString());
        }

        // Update configuration
        builder.Configuration[child.Key] = updatedValue;
    }
}
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddTransient<RequestLogContextMiddleware>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.Audience = builder.Configuration["Authentication:Audience"];
        o.MetadataAddress = builder.Configuration["Authentication:MetadataAddress"]!;

        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Authentication:ValidIssuer"],
        };
    });


var serilog = builder.Configuration.GetSection("Serilog");
var database = builder.Configuration.GetConnectionString("Database");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsePathBase("/api");

app.UseHttpsRedirection();

app.UseMiddleware<RequestLogContextMiddleware>();

app.MapCarter();

app.UseAuthentication();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.Run();