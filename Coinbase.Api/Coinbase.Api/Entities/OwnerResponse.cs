using Coinbase.Api.Entities;

namespace Coinbase.Api.Models
{
    public class OwnerResponse
    {
        public string Id { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public Role Role { get; set; }

        public IEnumerable<WalletResponse>? Wallets { get; set; }
    }
}
