namespace Coinbase.Api.Authorization
{
    public interface IJwtUtils
    {
        public int? ValidateJwtToken(string? token);
    }
}
