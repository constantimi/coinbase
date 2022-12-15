using Coinbase.Api.Data;
using Coinbase.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coinbase.Api.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly DataContext _context;

        public WalletRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateWalletAsync(Wallet wallet)
        {
            _context.Add(wallet);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync()
        {
            return await _context.Wallet.ToListAsync();
        }

        public async Task<Wallet> GetWalletByIdAsync(string id)
        {
            Wallet? wallet = await _context.Wallet.FirstOrDefaultAsync(w => w.ObjectId == id);
            if (wallet == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return wallet;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> UpdateWallet(Wallet wallet)
        {
            _context.Update(wallet);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteWallet(Wallet wallet)
        {
            _context.Remove(wallet);
            return await SaveChangesAsync();
        }

        public async Task<bool> WalletExists(string id)
        {
            return await _context.Wallet.AnyAsync(w => w.ObjectId == id);
        }
    }
}
