using Coinbase.Api.Models;

namespace Coinbase.Api.Entities
{
    public class RmqProducerResponse
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public Role Role { get; set; }

        public string Event { get; set; } = null!;
    }
}
