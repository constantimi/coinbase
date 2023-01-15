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

            // Sync Data Transfer
            CreateMap<Owner, CoinbaseOwnerResponse>();

            // Async Data Transfer
            CreateMap<Owner, PublisherRequest>();
        }
    }
}
