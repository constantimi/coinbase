using Coinbase.Api.Entities;

namespace Coinbase.Api.Repositories
{
    public interface IWalletRepository
    {
        bool CreateWallet(Wallet wallet);
        Task<bool> CreateWalletAsync(Wallet wallet);

        bool CreateWallet(int id, Wallet wallet);
        Task<bool> CreateWalletAsync(int id, Wallet wallet);

        bool DeleteWallet(string objectId);
        Task<bool> DeleteWalletAsync(string objectId);

        bool UpdateWallet(Wallet wallet);
        Task<bool> UpdateWalletAsync(Wallet wallet);

        bool SaveChanges();
        Task<bool> SaveChangesAsync();

        IEnumerable<Wallet> GetAllWalletsByOwnerId(int id);
        Task<IEnumerable<Wallet>> GetAllWalletsByOwnerIdAsync(int id);

        IEnumerable<Wallet> GetAllWallets();
        Task<IEnumerable<Wallet>> GetAllWalletsAsync();
    }
}
