using Coinbase.Services.Identity.Authorization;
using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Models;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Coinbase.Services.Identity.Services.AsyncDataServices;
using Coinbase.Services.Identity.Helpers;
using Coinbase.Services.Identity.SyncDataServices;
using Coinbase.Services.Identity.Services;

namespace Coinbase.Services.Identity.Controllers
{
    [Authorize]
    [ApiController]
    [Route("identity/owner")]
    public class OwnerController : ControllerBase
    {

        private readonly IIdentityDataClient _identityDataClient;
        private readonly IRmqMessageBusClient _messageBusClient;
        private readonly IOwnerService _ownerService;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerService ownerService,
                               IMapper mapper,
                               IIdentityDataClient identityDataClient,
                               IRmqMessageBusClient messageBusClient)
        {
            _ownerService = ownerService;
            _mapper = mapper;
            _identityDataClient = identityDataClient;
            _messageBusClient = messageBusClient;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerResponse>>> GetAllOwnersAsync()
        {
            IEnumerable<Owner> owners = await _ownerService.GetAllOwnersAsync();
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

            Owner owner = await _ownerService.GetOwnerByIdAsync(id);
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

            if (await _ownerService.CreateOwnerAsync(owner))
            {
                try
                {
                    // Send Sync Message
                    await _identityDataClient.SendIdentityToCoinbase(_mapper.Map<HttpOwnerResponse>(owner));

                    // Send Async Message
                    RmqProducerRequest publishedOwner = _mapper.Map<RmqProducerRequest>(owner);
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
