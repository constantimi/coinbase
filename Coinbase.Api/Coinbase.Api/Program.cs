using Coinbase.Api.Data;
using Coinbase.Api.Helpers;
using Coinbase.Api.Repositories;
using Coinbase.Api.Services.SyncDataServices.Http;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services
IServiceCollection services = builder.Services;

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
services.AddScoped<IOwnerRepository, OwnerRepository>();
services.AddScoped<IWalletRepository, WalletRepository>();

services.AddHttpClient<IIdentityDataClient, HttpIdentityDataClient>();


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(swagger => swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "CoinbaseService"));

// Global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

// Global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// Custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
