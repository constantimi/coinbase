using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Repositories;
using Coinbase.Services.Identity.Services;
using Telerik.JustMock;

namespace Coinbase.Services.Identity.Tests.Controllers
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
                   new Owner { Id = 0, FirstName = "", LastName = "", Email = "", Username = "u0", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass0"), Role = Role.Admin },
                   new Owner { Id = 1, FirstName = "", LastName = "", Email = "", Username = "u1", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass1"), Role = Role.Admin }
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
                .Returns((int id) => new Owner { Id = 1, FirstName = "", LastName = "", Email = "", Username = "u1", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass1"), Role = Role.Admin });

            var ownerService = new OwnerService(ownerRepository);
            const int requestedId = 1;

            // Act
            var owner = ownerService.GetOwnerById(requestedId);

            // Assert
            Assert.NotNull(owner);
            Assert.Equal(requestedId, owner.Id);
        }

        [Fact]
        public void OwnerService_GetOwnerById_ShouldReturnNull()
        {
            // Arrange
            var ownerRepository = Mock.Create<IOwnerRepository>();

            Mock.Arrange(() => ownerRepository.GetOwnerById(1))
                .Returns<Owner>(null);

            var ownerService = new OwnerService(ownerRepository);
            const int requestedId = 1;

            // Act
            var owner = ownerService.GetOwnerById(requestedId);

            // Assert
            Assert.Null(owner);
        }

        [Fact]
        public void OwnerService_GetOwnerById_ShouldReturnBadRequest()
        {
            // Arrange
            var ownerRepository = Mock.Create<IOwnerRepository>();

            Mock.Arrange(() => ownerRepository.GetOwnerById(0))
                .Returns((int id) => new Owner { Id = 0, FirstName="", LastName="", Email = "", Username = "u0", PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass0"), Role = Role.Admin });

            var ownerService = new OwnerService(ownerRepository);
            const int requestedId = 1;

            // Act
            var owner = ownerService.GetOwnerById(requestedId);

            // Assert
            Assert.NotNull(owner);
            Assert.False(owner.Id == requestedId);
        }
    }
}
