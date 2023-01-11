using Coinbase.Services.Identity.Models;

namespace Coinbase.Services.Identity.Services.SyncDataServices.Http
{
    public interface IIdentityDataClient
    {
        Task SendIdentityToCoinbase(CoinbaseOwnerResponse response);
    }
}
