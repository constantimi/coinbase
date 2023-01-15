using AutoMapper;
using Coinbase.Api.Entities;
using Coinbase.Api.Models;

namespace Coinbase.Api.Profiles
{
    public class CoinbaseProfile : Profile
    {
        public CoinbaseProfile()
        {
            CreateMap<Owner, OwnerResponse>();
            CreateMap<OwnerRequest, Owner>();

            CreateMap<Wallet, WalletResponse>();
            CreateMap<WalletRequest, Wallet>();

            // Async Data Transfer
            CreateMap<PublisherResponse, Owner>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
