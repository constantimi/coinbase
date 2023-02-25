using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Repositories;

namespace Coinbase.Services.Identity.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public bool CreateOwner(Owner owner)
        {
            return _ownerRepository.CreateOwner(owner);
        }

        public async Task<bool> CreateOwnerAsync(Owner owner)
        {
            return await _ownerRepository.CreateOwnerAsync(owner);
        }

        public bool DeleteOwner(Owner owner)
        {
            return _ownerRepository.DeleteOwner(owner);
        }

        public async Task<bool> DeleteOwnerAsync(Owner owner)
        {
            return await _ownerRepository.DeleteOwnerAsync(owner);
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _ownerRepository.GetAllOwners();
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await _ownerRepository.GetAllOwnersAsync();
        }

        public Owner GetOwnerById(int id)
        {
            return _ownerRepository.GetOwnerById(id);
        }

        public async Task<Owner> GetOwnerByIdAsync(int id)
        {
            return await _ownerRepository.GetOwnerByIdAsync(id);
        }

        public bool SaveChanges()
        {
            return _ownerRepository.SaveChanges();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _ownerRepository.SaveChangesAsync();
        }

        public bool UpdateOwner(Owner owner)
        {
            return _ownerRepository.UpdateOwner(owner);
        }

        public async Task<bool> UpdateOwnerAsync(Owner owner)
        {
            return await _ownerRepository.UpdateOwnerAsync(owner);
        }
    }
}
