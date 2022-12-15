using System.Text.Json.Serialization;

namespace Coinbase.Services.Identity.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
    }
}
