using Coinbase.Services.Identity.Models;

namespace Coinbase.Services.Identity.Services.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewOwner(PublisherRequest ownerPublishedRequest);
    }
}
