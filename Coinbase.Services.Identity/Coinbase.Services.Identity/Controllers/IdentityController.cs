using Coinbase.Services.Identity.Authorization;
using Coinbase.Services.Identity.Models;
using Coinbase.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coinbase.Services.Identity.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _jwtTokenService;

        public IdentityController(IIdentityService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            AuthenticateResponse response = _jwtTokenService.Authenticate(model);
            return Ok(response);
        }
    }
}
