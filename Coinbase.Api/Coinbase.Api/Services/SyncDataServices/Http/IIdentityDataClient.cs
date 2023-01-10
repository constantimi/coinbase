using Coinbase.Api.Models;

namespace Coinbase.Api.Services.SyncDataServices.Http
{
    public interface IIdentityDataClient
    {
        Task SendCoinbaseToIdentity(OwnerResponse ownerResponse);
    }
}