using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Helpers;
using System.Globalization;

namespace Coinbase.Services.Identity.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(Owner owner);
        public int? ValidateJwtToken(string token);
    }

    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;

        public JwtUtils(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateJwtToken(Owner owner)
        {
            // Generate token that is valid for 1 hour

            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            CultureInfo provider = new("en-us");
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", owner.Id.ToString(provider)) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public int? ValidateJwtToken(string token)
        {
            if (token == null)
            {
                return null;
            }

            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    // Set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken) validatedToken;

                CultureInfo provider = new("en-us");
                int ownerId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value, provider);

                // Return Owner id from JWT token if validation successful
                return ownerId;
            }
            catch
            {
                // Return null if validation fails
                return null;
            }
        }
    }
}
