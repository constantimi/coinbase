using System.ComponentModel.DataAnnotations;
using Coinbase.Api.Models;

namespace Coinbase.Api.Entities
{
    public class Owner
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public Role Role { get; set; }

        [Required]
        public IEnumerable<Wallet> Wallets { get; set; } = null!;

    }
}
