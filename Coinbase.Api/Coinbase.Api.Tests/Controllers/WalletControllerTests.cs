using AutoMapper;
using Coinbase.Api.Controllers;
using Coinbase.Api.Entities;
using Coinbase.Api.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Coinbase.Api.Tests.Controllers
{
    public class WalletControllerTests
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public WalletControllerTests()
        {
            _walletRepository = A.Fake<IWalletRepository>();
            _ownerRepository = A.Fake<IOwnerRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void WalletController_GetAllWallets_ReturnOK()
        {
            // Arrange
            var wallets = A.Fake<IEnumerable<Wallet>>();
            var response = A.Fake<IEnumerable<WalletResponse>>();

            A.CallTo(() => _mapper.Map<IEnumerable<WalletResponse>>(wallets)).Returns(response);

            var controller = new WalletController(_walletRepository,_ownerRepository, _mapper);

            // Act
            var actionResult = controller.GetAllWalletsAsync();

            // Assert
            var result = actionResult.Result;
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
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
