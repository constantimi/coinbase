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
        public void OwnerController_GetAllUsers_ReturnOK()
        {
            // Arrange
            var users = A.Fake<IEnumerable<Owner>>();
            var response = A.Fake<IEnumerable<OwnerResponse>>();

            A.CallTo(() => _mapper.Map<IEnumerable<OwnerResponse>>(users)).Returns(response);

            var controller = new OwnerController(_ownerRepository, _mapper);

            // Act
            var actionResult = controller.GetAllAsync();

            // Assert
            var result = actionResult.Result;
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }      
    }
}
