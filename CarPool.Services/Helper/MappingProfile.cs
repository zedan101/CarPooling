using AutoMapper;
using CarPool.Services.Data.Models;
using CarPool.Models;

namespace CarPool.Services.Helper
{
    public class MappingProfile : Profile
    {

        public MappingProfile() 
        {

            CreateMap<OfferedRide, Ride>()
                .ForPath(dest => dest.Location, act => act.MapFrom(src => src.Locations.OrderBy(loc => loc.SequenceNum).Select(loc => loc.Location.LocationName).ToList()))
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

            CreateMap<(Ride,User), DisplayRideRes>()
                .ForMember(dest => dest.UserName, act => act.MapFrom(src=> src.Item2.UserName))
                .ForMember(dest => dest.ProfileImage, act => act.MapFrom(src => src.Item2.ProfileImage))
                .ForMember(dest => dest.Time, act => act.MapFrom(src => src.Item1.Time))
                .ForMember(dest => dest.RideId, act => act.MapFrom(src => src.Item1.RideId))
                .ForMember(dest => dest.RideOfferedBy, act => act.MapFrom(src => src.Item1.RideOfferedBy))
                .ForMember(dest => dest.Date, act => act.MapFrom(src => src.Item1.Date))
                .ForMember(dest => dest.NumberOfSeatsAvailable, act => act.MapFrom(src => src.Item1.NumberOfSeatsAvailable))
                .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Item1.Price))
                .ForMember(dest => dest.StartLocation, act => act.MapFrom(src => src.Item1.Location[0]))
                .ForMember(dest => dest.EndLocation, act => act.MapFrom(src => src.Item1.Location[src.Item1.Location.Count()-1]));


        }
    }
}
