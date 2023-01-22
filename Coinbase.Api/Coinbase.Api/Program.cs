using Coinbase.Api.AsyncDataSubscriber;
using Coinbase.Api.Authorization;
using Coinbase.Api.Data;
using Coinbase.Api.EventProcessing;
using Coinbase.Api.Helpers;
using Coinbase.Api.Repositories;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services
IServiceCollection services = builder.Services;
IWebHostEnvironment env = builder.Environment;

services.AddCors();
services.AddDbContext<DataContext>();

services.AddControllers().AddJsonOptions(role =>
{
    // Serialize enums as strings in api responses (e.g. Role)
    role.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add MessageBusSubscriber
services.AddHostedService<RmqMessageBusConsumer>();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Configure strongly typed settings object
services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Configure DI for application services
services.AddScoped<IJwtUtils, JwtUtils>();
services.AddScoped<IOwnerRepository, OwnerRepository>();
services.AddScoped<IWalletRepository, WalletRepository>();

services.AddSingleton<IEventProcessor, EventProcessor>();

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
