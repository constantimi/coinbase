using Coinbase.Services.Identity.Authorization;
using Coinbase.Services.Identity.Models;
using Coinbase.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coinbase.Services.Identity.Controllers
{
    [Authorize]
    [ApiController]
    [Route("identity/authenticate")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _jwtTokenService;

        public IdentityController(IIdentityService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AuthenticateAsync(AuthenticateRequest model)
        {
            AuthenticateResponse response = _jwtTokenService.Authenticate(model);
            return Ok(response);
        }
    }
}
