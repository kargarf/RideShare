using AutoMapper;
using RideShare.Dtos;
using RideShare.Models;

namespace RideShare.Profiles
{
    public class TravelsProfile : Profile
    {
        public TravelsProfile()
        {
            // Source -> Target
            CreateMap<Travel, TravelReadDto>();
            CreateMap<TravelCreateDto, Travel>();
        }
    }
    
}