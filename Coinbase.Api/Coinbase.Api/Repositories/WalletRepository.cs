using System;
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
            if (!await WalletExists(wallet.ObjectId))
            {
                _context.Add(wallet);
                return await SaveChangesAsync();
            }

            return false;
        }

        public async Task<bool> CreateWalletAsync(int id, Wallet wallet)
        {
            if (!await WalletExists(wallet.ObjectId))
            {
                wallet.OwnerId = id;

                _context.Add(wallet);
                return await SaveChangesAsync();
            }

            return false;
        }

        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync()
        {
            return await _context.Wallet.ToListAsync();
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
            return _context.Wallet.ToList().Where(w => w.OwnerId == id);
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

        public async Task<bool> DeleteWallet(string objectId)
        {
            Wallet? wallet = await _context.Wallet.FirstOrDefaultAsync(w => w.ObjectId == objectId);
            if (wallet == null)
            {
                return false;
            }

            _context.Remove(wallet);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteWalletByObjectId(string objectId)
        {
            if (!await WalletExists(objectId))
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

        public async Task<bool> WalletExists(string id)
        {
            return await _context.Wallet.AnyAsync(w => w.ObjectId == id);
        }
    }
}
