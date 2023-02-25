using Coinbase.Services.Identity.Entities;

namespace Coinbase.Services.Identity.Services
{
    public interface IOwnerService
    {
        Owner GetOwnerById(int id);
        Task<Owner> GetOwnerByIdAsync(int id);

        IEnumerable<Owner> GetAllOwners();
        Task<IEnumerable<Owner>> GetAllOwnersAsync();

        bool CreateOwner(Owner owner);
        Task<bool> CreateOwnerAsync(Owner owner);

        bool DeleteOwner(Owner owner);
        Task<bool> DeleteOwnerAsync(Owner owner);

        bool UpdateOwner(Owner owner);
        Task<bool> UpdateOwnerAsync(Owner owner);

        bool SaveChanges();
        Task<bool> SaveChangesAsync();
    }
}
