using Coinbase.Api.Data;
using Coinbase.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coinbase.Api.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Owner> GetAllOwners()
        {
            return _context.Owners.Include(o => o.Wallets).ToList();
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await _context.Owners.Include(o => o.Wallets).ToListAsync();
        }

        public Owner GetOwnerById(int id)
        {
            return _context.Owners.Include(o => o.Wallets).First(o => o.ExternalId == id);
        }

        public async Task<Owner> GetOwnerByIdAsync(int id)
        {
            return await _context.Owners.Include(o => o.Wallets).FirstAsync(o => o.ExternalId == id).ConfigureAwait(false);
        }

        public bool CreateOwner(Owner owner)
        {
            if (!HelperExternalOwnerExists(owner.ExternalId))
            {
                _context.Add(owner);
                return SaveChanges();
            }

            return false;
        }

        public async Task<bool> CreateOwnerAsync(Owner owner)
        {
            if (!await HelperExternalOwnerExistsAsync(owner.ExternalId))
            {
                _context.Add(owner);
                return await SaveChangesAsync();
            }

            return false;
        }

        public bool DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            return SaveChanges();
        }
        public async Task<bool> DeleteOwnerAsync(Owner owner)
        {
            _context.Remove(owner);
            return await SaveChangesAsync();
        }

        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return SaveChanges();
        }

        public async Task<bool> UpdateOwnerAsync(Owner owner)
        {
            _context.Update(owner);
            return await SaveChangesAsync();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        private bool HelperExternalOwnerExists(int id)
        {
            return _context.Owners.Any(o => o.ExternalId == id);
        }

        private async Task<bool> HelperExternalOwnerExistsAsync(int id)
        {
            return await _context.Owners.AnyAsync(o => o.ExternalId == id);
        }
    }
}
