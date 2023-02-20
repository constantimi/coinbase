using Coinbase.Services.Identity.Controllers;
using Coinbase.Services.Identity.Models;
using Coinbase.Services.Identity.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Telerik.JustMock;

namespace Coinbase.Services.Identity.Tests.Controllers
{
    public class IdentityControllerTests
    {
        private readonly IIdentityService _jwtTokenService;

        public IdentityControllerTests()
        {
            _jwtTokenService = Mock.Create<IIdentityService>();
        }

        [Fact]
        public void IdentityController_Authenticate_ReturnOK()
        {
        }
    }
}
