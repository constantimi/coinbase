using Coinbase.Api.Entities;
using Coinbase.Api.Repositories;
using Coinbase.Api.Services;
using Telerik.JustMock;

namespace Coinbase.Api.Tests.Controllers
{
    public class WalletServiceTests
    {
        [Fact]
        public void WalletController_GetAllWallets_ReturnOK()
        {
            // Arrange
            var walletRepository = Mock.Create<IWalletRepository>();

            Mock.Arrange(() => walletRepository.GetAllWallets())
                .Returns(new List<Wallet>
                {
                    new Wallet { Id = 0, OwnerId = 1, ObjectId = "dfa41c48bb467425aa055b73eecb7630", BlockchainType = "ethereum", RecoveryPhrase="dfa41c48bb467425aa055b73eecb7630", WalletType = "eth" }, 
                    new Wallet { Id = 1, OwnerId = 1, ObjectId = "dfa41c48bb467425aa055b73eecb7631", BlockchainType = "ethereum", RecoveryPhrase="dfa41c48bb467425aa055b73eecb7631", WalletType = "eth" }, 
                    new Wallet { Id = 2, OwnerId = 1, ObjectId = "dfa41c48bb467425aa055b73eecb7632", BlockchainType = "ethereum", RecoveryPhrase="dfa41c48bb467425aa055b73eecb7632", WalletType = "eth" },
                });

            var walletService = new WalletService(walletRepository);
            
            // Act
            var wallets = walletService.GetAllWallets();

            // Assert
            Assert.NotNull(wallets);
            Assert.Equal(3, wallets.Count());
        }

        [Fact]
        public void WalletController_GetAllWallets_ShouldReturnNull()
        {
            // Arrange
            var walletRepository = Mock.Create<IWalletRepository>();

            Mock.Arrange(() => walletRepository.GetAllWallets())
                .Returns<IEnumerable<Wallet>>(null);

            var walletService = new WalletService(walletRepository);

            // Act
            var wallets = walletService.GetAllWallets();

            // Assert
            Assert.Null(wallets);
        }

        [Fact]
        public void WalletController_CreateWallet_ReturnOK()
        {
            // Arrange
            var walletRepository = Mock.Create<IWalletRepository>();

            var mockWallet = new Wallet { Id = 0, OwnerId = 1, ObjectId = "dfa41c48bb467425aa055b73eecb7630", BlockchainType = "ethereum", RecoveryPhrase = "dfa41c48bb467425aa055b73eecb7630", WalletType = "eth" };

            Mock.Arrange(() => walletRepository.CreateWallet(mockWallet))
                .Returns(true);

            var walletService = new WalletService(walletRepository);

            // Act
            var isWallet = walletService.CreateWallet(mockWallet);

            // Assert
            Assert.True(isWallet);
        }

        [Fact]
        public void WalletController_GetAllWalletsByOwnerId()
        {
            // Arrange
            var walletRepository = Mock.Create<IWalletRepository>();

            var mockWallet = new Wallet { Id = 0, OwnerId = 1, ObjectId = "dfa41c48bb467425aa055b73eecb7630", BlockchainType = "ethereum", RecoveryPhrase = "dfa41c48bb467425aa055b73eecb7630", WalletType = "eth" };

            Mock.Arrange(() => walletRepository.GetAllWalletsByOwnerId(1))
                .Returns(new List<Wallet> { mockWallet });

            var walletService = new WalletService(walletRepository);

            // Act
            var wallets = walletService.GetAllWalletsByOwnerId(1);

            // Assert
            Assert.Single(wallets);
            Assert.Equal(mockWallet.OwnerId, wallets.First().OwnerId);
        }

        [Fact]
        public void WalletController_DeleteWalletByObjectId()
        {
            // Arrange
            var walletRepository = Mock.Create<IWalletRepository>();

            var mockWallet = new Wallet { Id = 0, OwnerId = 1, ObjectId = "dfa41c48bb467425aa055b73eecb7630", BlockchainType = "ethereum", RecoveryPhrase = "dfa41c48bb467425aa055b73eecb7630", WalletType = "eth" };

            Mock.Arrange(() => walletRepository.DeleteWallet(mockWallet.ObjectId))
                .Returns(true);

            var walletService = new WalletService(walletRepository);

            // Act
            var isWallet = walletService.DeleteWallet(mockWallet.ObjectId);

            // Assert
            Assert.True(isWallet);
        }
    }
}
