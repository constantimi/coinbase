using Coinbase.Services.Identity.Models;

namespace Coinbase.Services.Identity.Services.AsyncDataServices
{
    public interface IRmqMessageBusClient
    {
        void InitializeRabbitMq();
        void PublishNewOwner(RmqProducerRequest rmqpProducerRequest);
    }
}
