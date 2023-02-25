using Coinbase.Services.Identity.Services;
using Telerik.JustMock;

namespace Coinbase.Services.Identity.Tests.Controllers
{
    public class IdentityServiceTests
    {
        private readonly IIdentityService _jwtTokenService;

        public IdentityServiceTests()
        {
            _jwtTokenService = Mock.Create<IIdentityService>();
        }

        [Fact]
        public void IdentityService_Authenticate_ReturnOK()
        {
        }
    }
}
