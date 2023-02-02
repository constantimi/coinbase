using FakeItEasy;
using AutoMapper;
using Coinbase.Api.Entities;
using Coinbase.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Coinbase.Api.Repositories;
using Coinbase.Api.Models;

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
            A.CallTo(() => _ownerRepository.GetAllOwnersAsync())
            .Returns(A.CollectionOfFake<Owner>(3));

            var controller = new OwnerController(_ownerRepository, _mapper);

            // Act
            var actionResult = controller.GetAllOwnersAsync();

            // Assert
            
            var result = actionResult.Result;
            result.Should().BeOfType<OkObjectResult>();
            result.Should().NotBeNull();
        }

        [Fact]
        public void OwnerController_CreateOwner_ReturnOK()
        {
            // Arrange
            var owner = A.Fake<Owner>();
            var response = A.Fake<IEnumerable<OwnerResponse>>();

            A.CallTo(() => _ownerRepository.CreateOwnerAsync(owner)).Returns(Task.FromResult(true));
            A.CallTo(() => _mapper.Map<IEnumerable<OwnerResponse>>(owner)).Returns(response);

            A.CallTo(() => _ownerRepository.CreateOwnerAsync(owner)).MustHaveHappenedOnceExactly();

            var controller = new OwnerController(_ownerRepository, _mapper);

            // Act
            var actionResult = controller.GetAllOwnersAsync();

            // Assert
            var result = actionResult.Result;
            result?.Value?.Count().Should().BeGreaterThan(0);
            result.Should().BeOfType<OkObjectResult>();
            result.Should().NotBeNull();
        }
    }
}
