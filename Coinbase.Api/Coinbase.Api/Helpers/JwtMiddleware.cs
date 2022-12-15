using Coinbase.Api.Repositories;
using Microsoft.Extensions.Options;

namespace Coinbase.Api.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context,IOwnerRepository ownerRepository, IJwtUtils jwtUtils)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int? ownerId = jwtUtils.ValidateJwtToken(token);
            if (ownerId != null)
            {
                // attach user to context on successful jwt validation
                // context.Items["Owner"] = await userRepository.GetByIdAsync(userId.Value);
                context.Items["Owner"] = await ownerRepository.GetOwnerByIdAsync(ownerId.Value);
            }

            await _next(context);
        }
    }
}
