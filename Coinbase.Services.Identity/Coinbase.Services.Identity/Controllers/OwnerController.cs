using Coinbase.Services.Identity.Authorization;
using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Repositories;
using Coinbase.Services.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Coinbase.Services.Identity.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerResponse>>> GetAllOwnersAsync()
        {
            IEnumerable<Owner> owners = await _ownerRepository.GetAllOwnersAsync();
            return Ok(_mapper.Map<IEnumerable<OwnerResponse>>(owners));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Owner>> GetOwnerByIdAsync(int id)
        {
            // only admins can access other Owner records
            Owner currentOwner = (Owner) HttpContext.Items["Owner"];
            if (id != currentOwner.Id && currentOwner.Role != Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            Owner owner = await _ownerRepository.GetOwnerByIdAsync(id);
            if (owner != null)
            {
                return Ok(_mapper.Map<OwnerResponse>(owner));
            }

            return NotFound();
        }
    }
}
