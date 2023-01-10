using Coinbase.Services.Identity.Repositories;

namespace Coinbase.Services.Identity.Authorization
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
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int? ownerId = jwtUtils.ValidateJwtToken(token);
            if (ownerId != null)
            {
                // attach Owner to context on successful jwt validation
                context.Items["Owner"] = await ownerRepository.GetOwnerByIdAsync(ownerId.Value);
            }

            await _next(context);
        }
    }
}
