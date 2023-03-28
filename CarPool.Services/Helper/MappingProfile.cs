using AutoMapper;
using CarPool.DataLayer.Models;
using CarPool.Models;


namespace CarPool.Services.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<OfferedRide, Ride>()
                .ForPath(dest => dest.Location, act => act.MapFrom(src => src.Locations.OrderBy(loc => loc.SequenceNum).Select(loc => loc.Location).ToList()))
                .ForPath(dest => dest.RideTakenBy, act => act.MapFrom(src => src.BookedRides.Select(br => br.UserId).ToList()))
                .ForMember(dest => dest.NumberOfSeatsAvailable, act => act.MapFrom(src => src.AvailableSeats))
                .ForMember(dest => dest.RideOfferedBy, act => act.MapFrom(src => src.UserId));

            CreateMap<Ride, OfferedRide>()
                .ForMember(dest => dest.AvailableSeats, act => act.MapFrom(src => src.NumberOfSeatsAvailable))
                .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.RideOfferedBy));

            CreateMap<UserEntity, User>()
                .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.Name));

            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.UserName));
        }
    }
}
