using Coinbase.Api.Entities;
using Coinbase.Api.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Coinbase.Api.Authorization;
using Coinbase.Api.Services;

namespace Coinbase.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("coinbase/owner")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerService ownerService, IMapper mapper)
        {
            _ownerService = ownerService;
            _mapper = mapper;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerResponse>>> GetAllOwnersAsync()
        {
            IEnumerable<Owner> owners = await _ownerService.GetAllOwnersAsync();
            return Ok(_mapper.Map<IEnumerable<OwnerResponse>>(owners));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OwnerResponse>> GetOwnerByIdAsync(int id)
        {
            Owner? owner = await _ownerService.GetOwnerByIdAsync(id);
            if (owner != null)
            {
                return Ok(_mapper.Map<OwnerResponse>(owner));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<OwnerResponse>> CreateOwnerAsync(OwnerRequest coinbaseRequest)
        {
            Owner owner = _mapper.Map<Owner>(coinbaseRequest);

            if (await _ownerService.CreateOwnerAsync(owner))
            {
                return Ok(_mapper.Map<OwnerResponse>(owner));
            }

            return BadRequest();
        }
    }
}
