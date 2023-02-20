using Coinbase.Api.Repositories;
using Telerik.JustMock;

namespace Coinbase.Api.Tests.Controllers
{
    public class WalletControllerTests
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IOwnerRepository _ownerRepository;

        public WalletControllerTests()
        {
            _walletRepository = Mock.Create<IWalletRepository>();
            _ownerRepository = Mock.Create<IOwnerRepository>();
        }

        [Fact]
        public void WalletController_GetAllWallets_ReturnOK()
        {
        }

        [Fact]
        public void WalletController_CreateWallet_ReturnOK_When_Wallet_Is_Created()
        {

        }

        [Fact]
        public void WalletController_GetAllWalletsByOwnerId()
        {

        }

        [Fact]
        public void WalletController_DeleteWalletByObjectId()
        {

        }
    }
}
