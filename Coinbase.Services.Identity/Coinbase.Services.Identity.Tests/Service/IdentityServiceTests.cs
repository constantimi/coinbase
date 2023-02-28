using Microsoft.EntityFrameworkCore;
using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Authorization;
using Coinbase.Services.Identity.Models;
using Coinbase.Services.Identity.Services;
using Coinbase.Services.Identity.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Coinbase.Services.Identity.Tests.Controllers
{
    public class IdentityServiceTests
    {
        [Fact]
        public void IdentityService_Authenticate_ReturnOK()
        {
            // Arrange
            var owner = new Owner { Id = 1, FirstName = "", LastName = "", Email = "", Username = "u0", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass0"), Role = Role.Admin };

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;


            using var context = new DataContext(options);
            context.Owners.Add(owner);
            context.SaveChanges();

            var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

            var appSettings = new AppSettings { Secret = "MockTestJwtSecret" };
            IOptions<AppSettings> appOptions = Options.Create(appSettings);

            var jwtUtils = new JwtUtils(appOptions);

            var mockAuthenticateRequest = new AuthenticateRequest { Username = "u0", Password = "pass0"};

            var identityService = new IdentityService(context, jwtUtils);
            
            // Act
            AuthenticateResponse result = identityService.Authenticate(mockAuthenticateRequest);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(owner.Id, jwtUtils.ValidateJwtToken(result.Token));
            Assert.Equal(owner.Id, result.Id);
            Assert.Equal(owner.FirstName, result.FirstName);
            Assert.Equal(owner.LastName, result.LastName);
            Assert.Equal(owner.Email, result.Email);
            Assert.Equal(owner.Role, result.Role);
        }
    }
}
