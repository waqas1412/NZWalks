using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto ,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto ,Region>().ReverseMap();
            CreateMap<AddWalksRequestDto ,Walk>().ReverseMap();
            CreateMap<WalksDto ,Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto ,Walk>().ReverseMap();
            CreateMap<Difficulty ,DifficultyDto>().ReverseMap();
        }
    }
}
