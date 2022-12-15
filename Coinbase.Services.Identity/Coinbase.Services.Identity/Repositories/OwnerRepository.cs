using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Coinbase.Services.Identity.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;
        private readonly AppSettings _appSettings;

        public OwnerRepository(DataContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<Owner> GetByIdAsync(int id)
        {
            Owner Owner = await _context.Owners.FindAsync(id);

            if (Owner == null)
            {
                throw new KeyNotFoundException("Owner not found");
            }

            return Owner;
        }
    }
}
