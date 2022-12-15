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
            Owner? owner = await _context.Owners.Include(o => o.Wallets).FirstOrDefaultAsync(o => o.Id == id);
            if (owner == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return owner;
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await _context.Owners.Include(o => o.Wallets).ToListAsync();
        }

        public async Task<bool> CreateOwnerAsync(Owner owner)
        {
            _context.Add(owner);
            return await SaveChangesAsync();
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

        public async Task<bool> OwnerExists(int id)
        {
            return await _context.Owners.AnyAsync(o => o.Id == id);
        }
    }
}
