using AutoMapper;
using Coinbase.Services.Identity.Entities;
using Coinbase.Services.Identity.Models;

namespace Coinbase.Services.Identity.Profiles
{
    public class IdentityProfiles : Profile
    {
        public IdentityProfiles()
        {
            CreateMap<Owner, OwnerResponse>();
            CreateMap<OwnerRequest, Owner>();

            CreateMap<Owner, CoinbaseOwnerResponse>();
        }
    }
}
