using Coinbase.Services.Identity.Entities;

namespace Coinbase.Services.Identity.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(Owner owner, string token)
        {
            Id = owner.Id;
            FirstName = owner.FirstName;
            LastName = owner.LastName;
            Email = owner.Email;
            Username = owner.Username;
            Role = owner.Role;
            Token = token;
        }
    }
}
