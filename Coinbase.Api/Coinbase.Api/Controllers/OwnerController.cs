using Coinbase.Api.Entities;
using Coinbase.Api.Helpers;
using Coinbase.Api.Models;
using Coinbase.Api.Repositories;
using Coinbase.Api.Services.SyncDataServices.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Coinbase.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        private readonly IIdentityDataClient _identityDataClient;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper, IIdentityDataClient identityDataClient)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;

            _identityDataClient = identityDataClient;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerResponse>>> GetAllOwnersAsync()
        {
            IEnumerable<Owner> owners = await _ownerRepository.GetAllOwnersAsync();
            return Ok(_mapper.Map<IEnumerable<OwnerResponse>>(owners));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OwnerResponse>> GetOwnerByIdAsync(int id)
        {
            // only admins can access other owner records
            Owner? currentOwner = HttpContext.Items["Owner"] as Owner;
            if (id != currentOwner?.Id && currentOwner?.Role != Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            Owner? owner = await _ownerRepository.GetOwnerByIdAsync(id);
            if (owner != null)
            {
                return Ok(_mapper.Map<OwnerResponse>(owner));
            }

            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<OwnerResponse>> CreateOwnerAsync(OwnerRequest coinbaseRequest)
        {
            Owner owner = _mapper.Map<Owner>(coinbaseRequest);
            await _ownerRepository.CreateOwnerAsync(owner);

            OwnerResponse coinbaseResponse = _mapper.Map<OwnerResponse>(owner);

            await _identityDataClient.SendCoinbaseToIdentity(coinbaseResponse);
            return Ok(_mapper.Map<OwnerResponse>(owner));
        }
    }
}
