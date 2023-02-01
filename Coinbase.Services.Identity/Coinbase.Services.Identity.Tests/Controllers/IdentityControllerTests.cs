using Coinbase.Services.Identity.Controllers;
using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Models;
using Coinbase.Services.Identity.Services;
using Coinbase.Services.Identity.SyncDataServices;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Coinbase.Services.Identity.Tests.Controllers
{
    public class IdentityControllerTests
    {
        private readonly IIdentityService _jwtTokenService;

        public IdentityControllerTests()
        {
            _jwtTokenService = A.Fake<IIdentityService>();
        }

        [Fact]
        public void IdentityController_Authenticate_ReturnOK()
        {
            // Arrange
            var response = A.Fake<AuthenticateResponse>();

            var controller = new IdentityController(_jwtTokenService);

            AuthenticateRequest authenticateRequest = A.Fake<AuthenticateRequest>();

            // Act
            var actionResult = controller.AuthenticateAsync(authenticateRequest);

            // Assert
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
