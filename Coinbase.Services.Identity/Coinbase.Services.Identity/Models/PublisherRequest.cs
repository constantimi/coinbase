using Coinbase.Services.Identity.Entities;

namespace Coinbase.Services.Identity.Models
{
    public class PublisherRequest
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public string Event { get; set; }
    }
}
