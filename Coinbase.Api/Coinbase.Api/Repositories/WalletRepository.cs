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

        public bool CreateWallet(Wallet wallet)
        {
            if (!HelperWalletExists(wallet.ObjectId))
            {
                _context.Add(wallet);
                return SaveChanges();
            }

            return false;
        }

        public async Task<bool> CreateWalletAsync(Wallet wallet)
        {
            if (!await HelperWalletExistsAsync(wallet.ObjectId))
            {
                _context.Add(wallet);
                return await SaveChangesAsync();
            }

            return false;
        }

        public bool CreateWallet(int id, Wallet wallet)
        {
            if (!HelperWalletExists(wallet.ObjectId))
            {
                wallet.OwnerId = id;

                _context.Add(wallet);
                return SaveChanges();
            }

            return false;
        }

        public async Task<bool> CreateWalletAsync(int id, Wallet wallet)
        {
            if (!await HelperWalletExistsAsync(wallet.ObjectId))
            {
                wallet.OwnerId = id;

                _context.Add(wallet);
                return await SaveChangesAsync();
            }

            return false;
        }

        public IEnumerable<Wallet> GetAllWallets()
        {
            return _context.Wallet.ToList();
        }

        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync()
        {
            return await _context.Wallet.ToListAsync();
        }

        public Wallet GetWalletByObjectId(string objectId)
        {
            return _context.Wallet.First(w => w.ObjectId == objectId);
        }

        public async Task<Wallet> GetWalletByObjectIdAsync(string objectId)
        {
            Wallet? wallet = await _context.Wallet.FirstOrDefaultAsync(w => w.ObjectId == objectId);
            if (wallet == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return wallet;
        }

        public IEnumerable<Wallet> GetAllWalletsByOwnerId(int id)
        {
            return _context.Wallet.Where(w => w.OwnerId == id).ToList();
        }

        public async Task<IEnumerable<Wallet>> GetAllWalletsByOwnerIdAsync(int id)
        {
            return await _context.Wallet.Where(w => w.OwnerId == id).ToListAsync();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public bool UpdateWallet(Wallet wallet)
        {
            _context.Update(wallet);
            return SaveChanges();
        }

        public async Task<bool> UpdateWalletAsync(Wallet wallet)
        {
            _context.Update(wallet);
            return await SaveChangesAsync();
        }

        public bool DeleteWallet(string objectId)
        {
            Wallet wallet = _context.Wallet.First(w => w.ObjectId == objectId);

            _context.Remove(wallet);
            return SaveChanges();
        }

        public async Task<bool> DeleteWalletAsync(string objectId)
        {
            Wallet wallet = await _context.Wallet.FirstAsync(w => w.ObjectId == objectId);

            _context.Remove(wallet);
            return await SaveChangesAsync();
        }

        public bool DeleteWalletByObjectId(string objectId)
        {
            if (!HelperWalletExists(objectId))
            {
                Wallet wallet = _context.Wallet.First(wallet => wallet.ObjectId == objectId);

                _context.Remove(wallet);
                return SaveChanges();
            }

            return false;
        }

        public async Task<bool> DeleteWalletByObjectIdAsync(string objectId)
        {
            if (!await HelperWalletExistsAsync(objectId))
            {
                Wallet? wallet = await _context.Wallet.FirstOrDefaultAsync(wallet => wallet.ObjectId == objectId);

                if (wallet != null)
                {
                    _context.Remove(wallet);
                    return await SaveChangesAsync();
                }
            }

            return false;
        }

        public bool HelperWalletExists(string id)
        {
            return _context.Wallet.Any(w => w.ObjectId == id);
        }

        public async Task<bool> HelperWalletExistsAsync(string id)
        {
            return await _context.Wallet.AnyAsync(w => w.ObjectId == id);
        }
    }
}
