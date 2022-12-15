using Coinbase.Api.Data;
using Coinbase.Api.Helpers;
using Coinbase.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    var services = builder.Services;
    var env = builder.Environment;

    var assembly = typeof(Program).Assembly.GetName().Name;
    var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    services.AddCors();
    services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(defaultConnectionString,
        sql => sql.MigrationsAssembly(assembly)));

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
}

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

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
