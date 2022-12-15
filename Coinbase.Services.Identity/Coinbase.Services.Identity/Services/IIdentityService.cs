using Coinbase.Services.Identity.Models;

namespace Coinbase.Services.Identity.Services
{
    public interface IIdentityService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}
