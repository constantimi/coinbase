using Coinbase.Services.Identity.Models;

namespace Coinbase.Services.Identity.SyncDataServices
{
    public interface IIdentityDataClient
    {
        Task SendIdentityToCoinbase(HttpOwnerResponse response);
    }
}
