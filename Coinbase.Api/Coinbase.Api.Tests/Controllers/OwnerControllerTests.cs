using AutoMapper;
using Coinbase.Api.Entities;
using Coinbase.Api.Controllers;
using FluentAssertions;
using Coinbase.Api.Repositories;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using Coinbase.Api.Models;

namespace Coinbase.Api.Tests.Controllers
{
    public class OwnerControllerTests
    {
        private IOwnerRepository _ownerRepository;
        private IMapper _mapper;

        public OwnerControllerTests()
        {
            _ownerRepository = Mock.Create<IOwnerRepository>();
            _mapper = Mock.Create<IMapper>();
        }

        [Fact]
        public void OwnerController_GetAllOwners_ReturnOK()
        {
            // Arrange
             _ownerRepository = Mock.Create<IOwnerRepository>();

            Mock.Arrange(() => _ownerRepository.GetAllOwnersAsync())
                .Returns(Task.FromResult<IEnumerable<Owner>>( new List<Owner>
                {
                    new Owner { Id = 0, ExternalId = 0, Email = "", Role = Role.Admin, Username = "u1", Wallets = Enumerable.Empty<Wallet>() },
                    new Owner { Id = 1, ExternalId = 0, Email = "", Role = Role.Admin, Username = "u2", Wallets = Enumerable.Empty<Wallet>() }
                }));

            var controller = new OwnerController(_ownerRepository, _mapper);

            // Act
            var owners = controller.GetAllOwnersAsync();

            // Assert
            Assert.NotNull(owners);
        }

        [Fact]
        public void OwnerController_CreateOwner_ReturnOK_When_Owner_Is_Created()
        {
        }
    }
}