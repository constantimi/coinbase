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

        public async Task<Owner> GetOwnerByIdAsync(int id)
        {
            Owner? owner = await _context.Owners.Include(o => o.Wallets).FirstOrDefaultAsync(o => o.ExternalId == id).ConfigureAwait(false);

            if (owner == null)
            {
                return new Owner();
            }

            return owner;
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await _context.Owners.Include(o => o.Wallets).ToListAsync();
        }

        public async Task<bool> CreateOwnerAsync(Owner owner)
        {
            if (!await ExternalOwnerExists(owner.ExternalId))
            {
                _context.Add(owner);
                return await SaveChangesAsync();
            }

            return false;
        }

        public async Task<bool> DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> ExternalOwnerExists(int id)
        {
            return await _context.Owners.AnyAsync(o => o.ExternalId == id);
        }
    }
}
