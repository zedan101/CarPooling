using AutoMapper;
using CarPool.DataLayer.Models;
using CarPool.Models;
using CarPool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public class MapperConfig : IMapperConfig
    {
        Mapper mapper;
        public Mapper OfferedRideToRide()
        {

            var offeredRideToRide = new MapperConfiguration(cfg =>
                cfg.CreateMap<OfferedRide, Ride>()
                .ForPath(dest => dest.Location, act => act.MapFrom(src => src.Locations.OrderBy(loc => loc.SequenceNum).Select(loc => loc.Location).ToList()))
                .ForPath(dest => dest.RideTakenBy, act => act.MapFrom(src => src.BookedRides.Select(br => br.UserId).ToList()))
                .ForMember(dest => dest.NumberOfSeatsAvailable, act => act.MapFrom(src => src.AvailableSeats))
                .ForMember(dest => dest.RideOfferedBy, act => act.MapFrom(src => src.UserId))
              );
            mapper = new Mapper(offeredRideToRide);
            return mapper;
        }

        public Mapper RideToOfferedRide()
        {

            var rideToOfferedRide = new MapperConfiguration(cfg =>
                cfg.CreateMap<Ride, OfferedRide>()
                .ForMember(dest => dest.AvailableSeats, act => act.MapFrom(src => src.NumberOfSeatsAvailable))
                .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.RideOfferedBy))
            );
            mapper = new Mapper(rideToOfferedRide);
            return mapper;
        }

        public Mapper UserEntityToUser()
        {
            var userEntityToUser = new MapperConfiguration(cfg =>
                cfg.CreateMap<UserEntity, User>()
                .ForMember(dest => dest.UserName,act => act.MapFrom(src=> src.Name))
            );
            mapper = new Mapper(userEntityToUser);
            return mapper;
        }

        public Mapper UserToUserEntity()
        {
            var userToUserEntity = new MapperConfiguration(cfg =>
                cfg.CreateMap<User, UserEntity>()
                .ForMember(dest => dest.Name,act => act.MapFrom(src => src.UserName))
            );
            mapper = new Mapper(userToUserEntity);
            return mapper;
        }
    }
}
