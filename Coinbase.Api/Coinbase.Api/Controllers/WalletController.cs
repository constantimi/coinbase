using AutoMapper;
using Coinbase.Api.Authorization;
using Coinbase.Api.Entities;
using Coinbase.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Coinbase.Api.Controllers
{
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

            if (await _ownerRepository.ExternalOwnerExists(id) && await _walletRepository.CreateWalletAsync(id, wallet))
            {
                return Ok(_mapper.Map<WalletResponse>(wallet));
            }

            return BadRequest();
        }

        [HttpGet("{ownerId:int}")]
        public async Task<ActionResult<IEnumerable<WalletResponse>>> GetAllWalletsByOwnerIdAsync(int ownerId)
        {
            IEnumerable<Wallet> walletList = await _walletRepository.GetAllWalletsByOwnerIdAsync(ownerId);
            if (walletList.Any())
            {
                return Ok(_mapper.Map<IEnumerable<WalletResponse>>(walletList));
            }

            return NotFound();
        }

        [HttpPost("delete/{objectId}")]
        public async Task<ActionResult> DeleteWalletByObjectIdAsync(string objectId)
        {
            if (await _walletRepository.DeleteWallet(objectId))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
