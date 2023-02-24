using Coinbase.Api.Entities;
using Coinbase.Api.Repositories;
using Telerik.JustMock;
using Coinbase.Api.Models;
using Coinbase.Api.Services;

namespace Coinbase.Api.Tests.Controllers
{
    public class OwnerServiceTests
    {
        [Fact]
        public void OwnerService_GetAllOwners_ReturnOK()
        {
            // Arrange
            var ownerRepository = Mock.Create<IOwnerRepository>();

            Mock.Arrange(() => ownerRepository.GetAllOwners())
                .Returns(new List<Owner>
                {
                    new Owner { Id = 0, ExternalId = 0, Email = "", Role = Role.Admin, Username = "u0", Wallets = Enumerable.Empty<Wallet>() },
                    new Owner { Id = 1, ExternalId = 1, Email = "", Role = Role.Admin, Username = "u1", Wallets = Enumerable.Empty<Wallet>() }
                });

            var ownerService = new OwnerService(ownerRepository);

            // Act
            var owners = ownerService.GetAllOwners();

            // Assert
            Assert.NotNull(owners);
            Assert.Equal(2, owners.Count());
        }

        [Fact]
        public void OwnerService_GetAllOwners_ShouldReturnNull()
        {
            // Arrange
            var ownerRepository = Mock.Create<IOwnerRepository>();

            Mock.Arrange(() => ownerRepository.GetAllOwners())
                .Returns<IEnumerable<Owner>>(null);

            var ownerService = new OwnerService(ownerRepository);

            // Act
            var owners = ownerService.GetAllOwners();

            // Assert
            Assert.Null(owners);
        }

        [Fact]
        public void OwnerService_GetOwnerById_ReturnOK()
        {
            // Arrange
            var ownerRepository = Mock.Create<IOwnerRepository>();

            Mock.Arrange(() => ownerRepository.GetOwnerById(1))
                .Returns((int id) => new Owner { Id = 1, ExternalId = 1, Email = "", Role = Role.Admin, Username = "u1", Wallets = Enumerable.Empty<Wallet>() });

            var ownerService = new OwnerService(ownerRepository);
            const int requestedOwnerId = 1;

            // Act
            var owner = ownerService.GetOwnerById(requestedOwnerId);

            // Assert
            Assert.NotNull(owner);
            Assert.Equal(requestedOwnerId, owner.ExternalId);
        }

        [Fact]
        public void OwnerService_GetOwnerById_ShouldReturnNull()
        {
            // Arrange
            var ownerRepository = Mock.Create<IOwnerRepository>();

            Mock.Arrange(() => ownerRepository.GetOwnerById(1))
                .Returns<Owner>(null);

            var ownerService = new OwnerService(ownerRepository);
            const int requestedOwnerId = 1;

            // Act
            var owner = ownerService.GetOwnerById(requestedOwnerId);

            // Assert
            Assert.Null(owner);
        }

        [Fact]
        public void OwnerService_GetOwnerById_ShouldReturnBadRequest()
        {
            // Arrange
            var ownerRepository = Mock.Create<IOwnerRepository>();

            Mock.Arrange(() => ownerRepository.GetOwnerById(0))
                .Returns((int id) => new Owner { Id = 0, ExternalId = 0, Email = "", Role = Role.Admin, Username = "u0", Wallets = Enumerable.Empty<Wallet>() });

            var ownerService = new OwnerService(ownerRepository);
            const int requestedOwnerId = 1;

            // Act
            var owner = ownerService.GetOwnerById(requestedOwnerId);

            // Assert
            Assert.NotNull(owner);
            Assert.False(owner.ExternalId == requestedOwnerId);
        }
    }
}