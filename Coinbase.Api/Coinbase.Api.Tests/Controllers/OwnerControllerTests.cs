using Coinbase.Api.Entities;
using Coinbase.Api.Repositories;
using Telerik.JustMock;
using Coinbase.Api.Models;

namespace Coinbase.Api.Tests.Controllers
{
    public class OwnerControllerTests
    {
        [Fact]
        public void OwnerController_GetAllOwners_ReturnOK()
        {
            // Arrange
            var ownerRepository = Mock.Create<IOwnerRepository>();

            Mock.Arrange(() => ownerRepository.GetAllOwners())
                .Returns(new List<Owner>
                {
                    new Owner { Id = 0, ExternalId = 0, Email = "", Role = Role.Admin, Username = "u1", Wallets = Enumerable.Empty<Wallet>() },
                    new Owner { Id = 1, ExternalId = 0, Email = "", Role = Role.Admin, Username = "u2", Wallets = Enumerable.Empty<Wallet>() }
                });

           /* var ownerService = new OwnerService(ownerRepository);

            // Act
            var actionResult = ownerService.GetAllOwners();

            // Assert
            var result = actionResult.Result as OkObjectResult;
            Assert.IsType<List<OwnerResponse>>(result.Value);

            var owners = result.Value as List<OwnerResponse>;
            Assert.NotNull(owners);
            Assert.Equal(2, owners.Count);*/
        }

        [Fact]
        public void OwnerController_CreateOwner_ReturnOK_When_Owner_Is_Created()
        {
        }
    }
}