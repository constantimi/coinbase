using Coinbase.Services.Identity.Entities;
using System.ComponentModel.DataAnnotations;

namespace Coinbase.Services.Identity.Models
{
    public class CoinbaseOwnerResponse
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public Role Role { get; set; }
    }
}
