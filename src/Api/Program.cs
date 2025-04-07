using Carter;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SavePlan.API;
using SavePlan.API.Identity;
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

builder.Services.AddAntiforgery(options => 
{
    options.HeaderName = "X-XSRF-TOKEN";
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});


var corsNamePolicy = "AllowFrontendApplication";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsNamePolicy, 
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// Identity services
builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddCookie(IdentityConstants.ApplicationScheme);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ExpireTimeSpan = TimeSpan.FromDays(15);
    options.Cookie.SameSite = SameSiteMode.Lax;
});

builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseCors(corsNamePolicy);

app.MapIdentityApi<User>()
    .DisableAntiforgery();

app.UsePathBase("/api");

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging();

app.MapCarter();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.Run();