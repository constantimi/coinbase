using System.Text.Json.Serialization;
using Coinbase.Services.Identity.Authorization;
using Coinbase.Services.Identity.Helpers;
using Coinbase.Services.Identity.Services;
using Coinbase.Services.Identity.Repositories;
using Coinbase.Services.Identity.Services.SyncDataServices.Http;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to DI container
IServiceCollection services = builder.Services;
IWebHostEnvironment env = builder.Environment;

services.AddCors();
services.AddDbContext<DataContext>();

services.AddControllers().AddJsonOptions(role =>
{
    // Serialize enums as strings in api responses (e.g. Role)
    role.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Configure strongly typed settings object
services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
services.AddScoped<IJwtUtils, JwtUtils>();
services.AddScoped<IIdentityService, IdentityService>();
services.AddScoped<IOwnerRepository, OwnerRepository>();

services.AddHttpClient<IIdentityDataClient, HttpIdentityDataClient>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (env.IsDevelopment() || env.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(swagger => swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityService"));
}

// Global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// app.UseHttpsRedirection();

// Global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// Custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
