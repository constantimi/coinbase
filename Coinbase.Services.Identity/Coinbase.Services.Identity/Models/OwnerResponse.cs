using Coinbase.Services.Identity.Entities;

namespace Coinbase.Services.Identity.Models
{
    public class OwnerResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public Role Role { get; set; }
    }
}
