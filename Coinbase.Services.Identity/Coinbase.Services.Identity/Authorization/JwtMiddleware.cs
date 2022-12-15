using Microsoft.Extensions.Options;
using Coinbase.Services.Identity.Helpers;
using Coinbase.Services.Identity.Repositories;

namespace Coinbase.Services.Identity.Authorization
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

        public async Task Invoke(HttpContext context, IOwnerRepository OwnerRepository, IJwtUtils jwtUtils)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int? OwnerId = jwtUtils.ValidateJwtToken(token);
            if (OwnerId != null)
            {
                // attach Owner to context on successful jwt validation
                context.Items["Owner"] = await OwnerRepository.GetByIdAsync(OwnerId.Value);
            }

            await _next(context);
        }
    }
}
