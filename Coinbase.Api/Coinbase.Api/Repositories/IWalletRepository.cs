using Coinbase.Api.Entities;

namespace Coinbase.Api.Repositories
{
    public interface IWalletRepository
    {
        Task<Wallet> GetWalletByIdAsync(string id);

        Task<IEnumerable<Wallet>> GetAllWalletsAsync();

        Task<bool> CreateWalletAsync(Wallet wallet);

        Task<bool> DeleteWallet(Wallet wallet);

        Task<bool> UpdateWallet(Wallet wallet);

        Task<bool> SaveChangesAsync();

        Task<bool> WalletExists(string id);
    }
}
