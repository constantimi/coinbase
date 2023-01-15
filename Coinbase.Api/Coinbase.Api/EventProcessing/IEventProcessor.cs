namespace Coinbase.Api.EventProcessing
{
    public interface IEventProcessor
    {
        Task ProcessEventAsync(string message);
    }
}
