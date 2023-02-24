using Coinbase.Api.Entities;
using Coinbase.Api.Repositories;

namespace Coinbase.Api.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public bool CreateWallet(Wallet wallet)
        {
            return _walletRepository.CreateWallet(wallet);
        }

        public bool CreateWallet(int id, Wallet wallet)
        {
            return _walletRepository.CreateWallet(id, wallet);
        }

        public async Task<bool> CreateWalletAsync(Wallet wallet)
        {
            return await _walletRepository.CreateWalletAsync(wallet);
        }

        public async Task<bool> CreateWalletAsync(int id, Wallet wallet)
        {
            return await _walletRepository.CreateWalletAsync(id, wallet);
        }

        public bool DeleteWallet(string objectId)
        {
            return _walletRepository.DeleteWallet(objectId);
        }

        public async Task<bool> DeleteWalletAsync(string objectId)
        {
            return await _walletRepository.DeleteWalletAsync(objectId);
        }

        public IEnumerable<Wallet> GetAllWallets()
        {
            return _walletRepository.GetAllWallets();
        }

        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync()
        {
            return await _walletRepository.GetAllWalletsAsync();
        }

        public IEnumerable<Wallet> GetAllWalletsByOwnerId(int id)
        {
            return _walletRepository.GetAllWalletsByOwnerId(id);
        }

        public async Task<IEnumerable<Wallet>> GetAllWalletsByOwnerIdAsync(int id)
        {
            return await _walletRepository.GetAllWalletsByOwnerIdAsync(id);
        }

        public bool SaveChanges()
        {
            return _walletRepository.SaveChanges();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _walletRepository.SaveChangesAsync();
        }

        public bool UpdateWallet(Wallet wallet)
        {
            return _walletRepository.UpdateWallet(wallet);
        }

        public async Task<bool> UpdateWalletAsync(Wallet wallet)
        {
            return await _walletRepository.UpdateWalletAsync(wallet);
        }
    }
}
