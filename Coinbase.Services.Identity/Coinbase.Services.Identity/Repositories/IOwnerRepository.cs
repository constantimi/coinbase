using Coinbase.Services.Identity.Entities;

namespace Coinbase.Services.Identity.Repositories
{
    public interface IOwnerRepository
    {
        Task<Owner> GetOwnerByIdAsync(int id);

        Task<IEnumerable<Owner>> GetAllOwnersAsync();

        Task<bool> CreateOwnerAsync(Owner owner);

        Task<bool> DeleteOwner(Owner owner);

        Task<bool> UpdateOwner(Owner owner);

        Task<bool> SaveChangesAsync();

        Task<bool> OwnerExists(string username);
    }
}
