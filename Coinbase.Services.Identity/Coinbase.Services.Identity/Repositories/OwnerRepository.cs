using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Coinbase.Services.Identity.Repositories
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
            Owner owner = await _context.Owners.FirstOrDefaultAsync(o => o.Id == id).ConfigureAwait(false);

            if (owner == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return owner;
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<bool> CreateOwnerAsync(Owner owner)
        {
            if (await OwnerExists(owner.Username))
            {
                return false;
            }

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

        public async Task<bool> OwnerExists(string username)
        {
            return await _context.Owners.AnyAsync(o => o.Username == username);
        }
    }
}
