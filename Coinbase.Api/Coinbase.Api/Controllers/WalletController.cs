using AutoMapper;
using Coinbase.Api.Entities;
using Coinbase.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coinbase.Api.Controllers
{
    [ApiController]
    [Route("coinbase/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public WalletController(IWalletService walletService, IMapper mapper)
        {
            _walletService = walletService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WalletResponse>>> GetAllWalletsAsync()
        {
            IEnumerable<Wallet> wallets = await _walletService.GetAllWalletsAsync();
            return Ok(_mapper.Map<IEnumerable<WalletResponse>>(wallets));
        }

        [HttpPost("{id:int}")]
        public async Task<ActionResult<WalletResponse>> CreateWalletAsync(int id, WalletRequest walletRequest)
        {
            Wallet wallet = _mapper.Map<Wallet>(walletRequest);

            if (await _walletService.CreateWalletAsync(id, wallet))
            {
                return Ok(_mapper.Map<WalletResponse>(wallet));
            }

            return BadRequest();
        }

        [HttpGet("{ownerId:int}")]
        public async Task<ActionResult<IEnumerable<WalletResponse>>> GetAllWalletsByOwnerIdAsync(int ownerId)
        {
            IEnumerable<Wallet> walletList = await _walletService.GetAllWalletsByOwnerIdAsync(ownerId);
            if (walletList.Any())
            {
                return Ok(_mapper.Map<IEnumerable<WalletResponse>>(walletList));
            }

            return NotFound();
        }

        [HttpPost("delete/{objectId}")]
        public async Task<ActionResult> DeleteWalletByObjectIdAsync(string objectId)
        {
            if (await _walletService.DeleteWalletAsync(objectId))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
