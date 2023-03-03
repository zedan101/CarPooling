using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.Services.Interfaces;

namespace CarPool.Services
{
    public class RidesService : IRidesService
    {
            public List<Rides> GetMatches(string date, string time, string startLocation, string destination)
            {
                return GlobalStorage.Rides.FindAll(rides => rides.Date == date && rides.Time == time && rides.Location.First() == startLocation && rides.Location.Any(loc => loc == destination)).ToList();
            }

            public bool OfferRide(Rides ride)
            {
                GlobalStorage.Rides.Add(ride);
                return true;
            }

            public List<Rides> GetRideHistory(string userId)
            {
                return GlobalStorage.Rides.Where(ride => ride.RideTakenBy.Any(id => id==userId) || ride.RideOfferedBy == userId).ToList();
            }
    }
}

