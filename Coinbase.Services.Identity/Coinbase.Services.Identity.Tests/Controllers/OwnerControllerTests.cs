using AutoMapper;
using Coinbase.Services.Identity.Controllers;
using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Models;
using Coinbase.Services.Identity.Repositories;
using Coinbase.Services.Identity.Services.AsyncDataServices;
using Coinbase.Services.Identity.SyncDataServices;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbase.Services.Identity.Tests.Controllers
{
    public class OwnerControllerTests
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IRmqMessageBusClient _messageBusClient;
        private readonly IIdentityDataClient _identityDataClient;
        private readonly IMapper _mapper;

        public OwnerControllerTests()
        {
            _ownerRepository = A.Fake<IOwnerRepository>();
            _mapper = A.Fake<IMapper>();
            _identityDataClient = A.Fake<IIdentityDataClient>();
            _messageBusClient = A.Fake<IRmqMessageBusClient>();
        }

        [Fact]
        public void OwnerController_GetAllOwners_ReturnOK()
        {
            // Arrange
            var owners = A.Fake<IEnumerable<Owner>>();
            var response = A.Fake<IEnumerable<OwnerResponse>>();

            A.CallTo(() => _mapper.Map<IEnumerable<OwnerResponse>>(owners)).Returns(response);

            var controller = new OwnerController(_ownerRepository, _mapper, _identityDataClient, _messageBusClient);

            // Act
            var actionResult = controller.GetAllOwnersAsync();

            // Assert
            var result = actionResult.Result;
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void OwnerController_CreateOwner_ReturnOK_When_Owner_Is_Created()
        {

        }

        [Fact]
        public void OwnerController_CreateOwner_Returns_JWT_When_Created()
        {

        }
    }
}
