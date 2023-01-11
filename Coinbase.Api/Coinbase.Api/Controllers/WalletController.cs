using AutoMapper;
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
        private readonly IMapper _mapper;
        public WalletController(IWalletRepository walletRepository, IMapper mapper)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WalletResponse>>> GetAllAsync()
        {
            IEnumerable<Wallet> wallets = await _walletRepository.GetAllWalletsAsync();
            return Ok(_mapper.Map<IEnumerable<WalletResponse>>(wallets));
        }
    }
}
