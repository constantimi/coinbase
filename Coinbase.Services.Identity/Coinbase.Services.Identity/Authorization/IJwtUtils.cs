using Coinbase.Services.Identity.Entities;

namespace Coinbase.Services.Identity.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(Owner owner);
        public int? ValidateJwtToken(string token);
    }
}
