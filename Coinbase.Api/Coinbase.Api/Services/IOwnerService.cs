using Coinbase.Api.Entities;

namespace Coinbase.Api.Services
{
    public interface IOwnerService
    {
        IEnumerable<Owner> GetAllOwners();
        Task<IEnumerable<Owner>> GetAllOwnersAsync();

        Owner GetOwnerById(int id);
        Task<Owner> GetOwnerByIdAsync(int id);

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
