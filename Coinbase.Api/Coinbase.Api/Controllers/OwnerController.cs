using AutoMapper;
using Coinbase.Api.Entities;
using Coinbase.Api.Helpers;
using Coinbase.Api.Models;
using Coinbase.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Coinbase.Api.Controllers
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
        public async Task<ActionResult<IEnumerable<OwnerResponse>>> GetAllAsync()
        {
            IEnumerable<Owner> users = await _ownerRepository.GetAllOwnersAsync();
            return Ok(_mapper.Map<IEnumerable<OwnerResponse>>(users));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OwnerResponse>> GetUserByIdAsync(int id)
        {
            // only admins can access other user records
            Owner currentOwner = (Owner) HttpContext.Items["Owner"];
            if (id != currentOwner.Id && currentOwner.Role != Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }


            Owner? user = await _ownerRepository.GetOwnerByIdAsync(id);
            if (user != null)
            {
                return Ok(_mapper.Map<OwnerResponse>(user));
            }

            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<OwnerResponse>> CreateUserAsync(OwnerRequest CoinbaseRequest)
        {
            Owner user = _mapper.Map<Owner>(CoinbaseRequest);
            await _ownerRepository.CreateOwnerAsync(user);

            OwnerResponse CoinbaseResponse = _mapper.Map<OwnerResponse>(user);

            // return CreatedAtRoute(nameof(GetUserByIdAsync), new { CoinbaseResponse.Id }, CoinbaseResponse);

            return Ok(_mapper.Map<OwnerResponse>(user));
        }
    }
}
