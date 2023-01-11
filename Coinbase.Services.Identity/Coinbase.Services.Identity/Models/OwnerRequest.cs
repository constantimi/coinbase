using Coinbase.Services.Identity.Entities;
using System.ComponentModel.DataAnnotations;

namespace Coinbase.Services.Identity.Models
{
    public class OwnerRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
