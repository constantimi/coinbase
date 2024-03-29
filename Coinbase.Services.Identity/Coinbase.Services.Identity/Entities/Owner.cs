using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Coinbase.Services.Identity.Entities
{
    public class Owner
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [JsonIgnore]
        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
