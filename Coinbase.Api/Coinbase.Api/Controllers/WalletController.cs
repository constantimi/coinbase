using AutoMapper;
using Coinbase.Api.Authorization;
using Coinbase.Api.Entities;
using Coinbase.Api.Models;
using Coinbase.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Coinbase.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("coinbase/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        public WalletController(IWalletRepository walletRepository, IOwnerRepository ownerRepository, IMapper mapper)
        {
            _walletRepository = walletRepository;
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WalletResponse>>> GetAllAsync()
        {
            IEnumerable<Wallet> wallets = await _walletRepository.GetAllWalletsAsync();
            return Ok(_mapper.Map<IEnumerable<WalletResponse>>(wallets));
        }

        [HttpPost("{id:int}")]
        public async Task<ActionResult<WalletResponse>> CreateWalletAsync(int id, WalletRequest walletRequest)
        {
            Wallet wallet = _mapper.Map<Wallet>(walletRequest);

            if (await _ownerRepository.ExternalOwnerExists(id))
            {
                if (await _walletRepository.CreateWalletAsync(id, wallet))
                {
                    return Ok(_mapper.Map<WalletResponse>(wallet));
                }
            }

            return BadRequest();
        }
    }
}
