using AutoMapper;
using Coinbase.Services.Identity.Repositories;
using Coinbase.Services.Identity.Services.AsyncDataServices;
using Coinbase.Services.Identity.SyncDataServices;
using Telerik.JustMock;

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
            _ownerRepository = Mock.Create<IOwnerRepository>();
            _mapper = Mock.Create<IMapper>();
            _identityDataClient = Mock.Create<IIdentityDataClient>();
            _messageBusClient = Mock.Create<IRmqMessageBusClient>();
        }

        [Fact]
        public void OwnerController_GetAllOwners_ReturnOK()
        {
            
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
