using Coinbase.Api.Repositories;

namespace Coinbase.Api.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IOwnerRepository ownerRepository, IJwtUtils jwtUtils)
        {
            string token = context.Request.Headers.FirstOrDefault(h => h.Key.Equals("Authorization", StringComparison.Ordinal)).Value.ToString().Split(" ").Last();

            int? ownerId = jwtUtils.ValidateJwtToken(token);
            if (ownerId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["Owner"] = await ownerRepository.GetOwnerByIdAsync(ownerId.Value);
            }

            await _next(context);
        }
    }
}
