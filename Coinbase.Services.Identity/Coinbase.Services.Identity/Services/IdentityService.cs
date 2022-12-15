using Coinbase.Services.Identity.Authorization;
using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Helpers;
using Coinbase.Services.Identity.Models;

namespace Coinbase.Services.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly DataContext _context;
        private readonly IJwtUtils _jwtUtils;

        public IdentityService(DataContext context, IJwtUtils jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            Owner owner = _context.Owners.SingleOrDefault(x => x.Username == model.Username);

            // Validate
            if (owner == null || !BCrypt.Net.BCrypt.Verify(model.Password, owner.PasswordHash))
            {
                throw new AppException("Ownername or password is incorrect");
            }

            // authentication successful so generate jwt token
            string jwtToken = _jwtUtils.GenerateJwtToken(owner);

            return new AuthenticateResponse(owner, jwtToken);
        }
    }
}
