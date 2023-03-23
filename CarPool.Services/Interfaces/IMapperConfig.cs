using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Interfaces
{
    public interface IMapperConfig
    {
        public Mapper OfferedRideToRide();
        public Mapper RideToOfferedRide();
        public Mapper UserEntityToUser();
        public Mapper UserToUserEntity();

    }
}
