using FakeItEasy;
using AutoMapper;
using Coinbase.Api.Entities;
using Coinbase.Api.Models;
using Coinbase.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Coinbase.Api.Repositories;

namespace Coinbase.Api.Tests.Controllers
{
    public class OwnerControllerTests
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerControllerTests()
        {
            _ownerRepository = A.Fake<IOwnerRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void OwnerController_GetAllOwners_ReturnOK()
        {
            // Arrange
            var owners = A.Fake<IEnumerable<Owner>>();
            var response = A.Fake<IEnumerable<OwnerResponse>>();

            A.CallTo(() => _mapper.Map<IEnumerable<OwnerResponse>>(owners)).Returns(response);

            var controller = new OwnerController(_ownerRepository, _mapper);

            // Act
            var actionResult = controller.GetAllOwnersAsync();

            // Assert
            var result = actionResult.Result;
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void OwnerController_GetAllOwners_ReturnOwners()
        {
            // Arrange
            var owners = Helper_CreatedOwnersDefault();
            var repository = A.Fake<IOwnerRepository>();
            bool isCreated = true;

            A.CallTo(() => repository.CreateOwnerAsync(owners.First())).Returns(isCreated);

            var controller = new OwnerController(_ownerRepository, _mapper);

            // Act
            var actionResult = controller.GetAllOwnersAsync();

            // Assert
            var result = actionResult.Result;
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        private IEnumerable<Owner> Helper_CreatedOwnersDefault()
        {
            return new List<Owner>
            {
                new Owner{ Id = 0, ExternalId = 0, Username = "owner0", Email = "owner0", Role = Role.User, Wallets = Enumerable.Empty<Wallet>() },
                new Owner{ Id = 1, ExternalId = 1, Username = "owner1", Email = "owner1", Role = Role.User, Wallets = Enumerable.Empty<Wallet>() },
                new Owner{ Id = 2, ExternalId = 2, Username = "owner2", Email = "owner2", Role = Role.User , Wallets = Enumerable.Empty <Wallet>() }
            };
        }

        [Fact]
        public void OwnerController_GetOwnerById_ReturnOK()
        {
            // Arrange
            var owners = Helper_CreatedOwnersDefault();
            var response = A.Fake<IEnumerable<OwnerResponse>>();

            A.CallTo(() => _mapper.Map<IEnumerable<OwnerResponse>>(owners)).Returns(response);

            var controller = new OwnerController(_ownerRepository, _mapper);

            // Act
            var actionResult = controller.GetOwnerByIdAsync(owners.First().ExternalId);

            // Assert
            var result = actionResult.Result;
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
