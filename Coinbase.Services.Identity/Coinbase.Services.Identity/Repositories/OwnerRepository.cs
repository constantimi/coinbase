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

        public Owner GetOwnerById(int id)
        {
            return _context.Owners.First(o => o.Id == id);
        }

        public async Task<Owner> GetOwnerByIdAsync(int id)
        {
            return await _context.Owners.FirstAsync(o => o.Id == id).ConfigureAwait(false);
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _context.Owners.ToList();
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await _context.Owners.ToListAsync();
        }

        public bool CreateOwner(Owner owner)
        {
            if (!HelperOwnerExists(owner.Username))
            {
                _context.Add(owner);
                return SaveChanges();
            }

            return false;
        }

        public async Task<bool> CreateOwnerAsync(Owner owner)
        {
            if (!await HelperOwnerExistsAsync(owner.Username))
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

        private bool HelperOwnerExists(string username)
        {
            return _context.Owners.Any(o => o.Username == username);
        }

        private async Task<bool> HelperOwnerExistsAsync(string username)
        {
            return await _context.Owners.AnyAsync(o => o.Username == username);
        }
    }
}
