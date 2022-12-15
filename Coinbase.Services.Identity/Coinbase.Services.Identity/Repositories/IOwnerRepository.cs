using Coinbase.Services.Identity.Entities;

namespace Coinbase.Services.Identity.Repositories
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllAsync();
        Task<Owner> GetByIdAsync(int id);
    }
}
