namespace Coinbase.Api.Helpers
{
    public interface IJwtUtils
    {
        public int? ValidateJwtToken(string token);
    }
}
