using Microsoft.AspNetCore.Mvc;
using Coinbase.Services.Identity.Authorization;
using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Repositories;

namespace Coinbase.Services.Identity.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _OwnerRepository;

        public OwnerController(IOwnerRepository OwnerRepository)
        {
            _OwnerRepository = OwnerRepository;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetAllAsync()
        {
            IEnumerable<Owner> Owners = await _OwnerRepository.GetAllAsync();
            return Ok(Owners);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Owner>> GetByIdAsync(int id)
        {
            // only admins can access other Owner records
            Owner currentOwner = (Owner) HttpContext.Items["Owner"];
            if (id != currentOwner.Id && currentOwner.Role != Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            Owner Owner = await _OwnerRepository.GetByIdAsync(id);
            return Ok(Owner);
        }
    }
}
