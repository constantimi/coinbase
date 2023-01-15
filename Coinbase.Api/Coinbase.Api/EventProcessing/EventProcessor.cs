using System.Text.Json;
using AutoMapper;
using Coinbase.Api.Entities;
using Coinbase.Api.Helpers;
using Coinbase.Api.Repositories;

namespace Coinbase.Api.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public async Task ProcessEventAsync(string message)
        {
            EventType eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.OwnerPublished:
                    await AddOwnerAsync(message);
                    break;
                case EventType.Undetermined:
                    break;
                default:
                    break;
            }
        }

        private static EventType DetermineEvent(string notifcationMessage)
        {
            GenericEvent? eventType = JsonSerializer.Deserialize<GenericEvent>(notifcationMessage);

            if (eventType == null)
            {
                return EventType.Undetermined;
            }

            return eventType.Event switch
            {
                "Owner_Published" => EventType.OwnerPublished,
                _ => EventType.Undetermined,
            };
        }
        private async Task AddOwnerAsync(string message)
        {
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                IOwnerRepository ownerReporistory = scope.ServiceProvider.GetRequiredService<IOwnerRepository>();

                PublisherResponse? publishedOwner = JsonSerializer.Deserialize<PublisherResponse>(message);

                try
                {
                    Owner owner = _mapper.Map<Owner>(publishedOwner);
                    await ownerReporistory.CreateOwnerAsync(owner);
                }
                catch (AppException ex)
                {
                    throw new AppException($"Could not add Owner to the Database {ex.Message}");
                }
            }
        }
    }

    internal enum EventType
    {
        OwnerPublished,
        Undetermined
    }
}
