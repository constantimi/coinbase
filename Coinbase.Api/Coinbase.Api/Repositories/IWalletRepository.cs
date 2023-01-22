using Coinbase.Api.Entities;

namespace Coinbase.Api.Repositories
{
    public interface IWalletRepository
    {
        Task<IEnumerable<Wallet>> GetAllWalletsByOwnerIdAsync(int id);

        Task<IEnumerable<Wallet>> GetAllWalletsAsync();

        Task<bool> CreateWalletAsync(Wallet wallet);

        Task<bool> CreateWalletAsync(int id, Wallet wallet);

        Task<bool> DeleteWalletByObjectId(string objectId);

        Task<bool> DeleteWallet(string objectId);

        Task<bool> UpdateWallet(Wallet wallet);

        Task<bool> SaveChangesAsync();

        Task<bool> WalletExists(string id);
    }
}
