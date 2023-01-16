using Coinbase.Services.Identity.Authorization;
using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Repositories;
using Coinbase.Services.Identity.Models;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Coinbase.Services.Identity.Services.SyncDataServices.Http;
using Coinbase.Services.Identity.Services.AsyncDataServices;
using Coinbase.Services.Identity.Helpers;

namespace Coinbase.Services.Identity.Controllers
{
    [Authorize]
    [ApiController]
    [Route("identity/owner")]
    public class OwnerController : ControllerBase
    {

        private readonly IIdentityDataClient _identityDataClient;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository,
                               IMapper mapper,
                               IIdentityDataClient identityDataClient,
                               IMessageBusClient messageBusClient)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
            _identityDataClient = identityDataClient;
            _messageBusClient = messageBusClient;
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<OwnerResponse>> CreateOwnerAsync(OwnerRequest coinbaseRequest)
        {
            Owner owner = _mapper.Map<Owner>(coinbaseRequest);
            owner.PasswordHash = BCryptNet.HashPassword(coinbaseRequest.Password);

            if (await _ownerRepository.CreateOwnerAsync(owner))
            {
                try
                {
                    // Send Sync Message
                    await _identityDataClient.SendIdentityToCoinbase(_mapper.Map<CoinbaseOwnerResponse>(owner));

                    // Send Async Message
                    PublisherRequest publishedOwner = _mapper.Map<PublisherRequest>(owner);
                    publishedOwner.Event = "Owner_Published";
                    _messageBusClient.PublishNewOwner(publishedOwner);
                }
                catch (AppException)
                {
                    throw new AppException();
                }

                return Ok(_mapper.Map<OwnerResponse>(owner));
            }

            return BadRequest();
        }
    }
}
