using System.ComponentModel.DataAnnotations;
using Coinbase.Api.Entities;

namespace Coinbase.Api.Models
{
    public class OwnerRequest
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public Role Role { get; set; }

        public IEnumerable<WalletRequest>? Wallets { get; set; }
    }
}
