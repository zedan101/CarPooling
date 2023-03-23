
using AutoMapper;
using Carpool.DataLayer;
using CarPool.DataLayer.Models;
using CarPool.Models;
using CarPool.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarPool.Services
{
    public class RidesService : IRidesService
    {

        private readonly CarPoolContext _carPoolContext;
        private readonly IMapperConfig _mapperConfig;
        public RidesService(CarPoolContext carPoolContext, IMapperConfig mapConfig)
        {
            _carPoolContext = carPoolContext;
            _mapperConfig = mapConfig;
        }

        

        MapperConfig mapperConfig = new();




        /// <summary>
        /// Method to get user input and match the input with available rides and send back the matched rides.
        /// </summary>
        /// <param name="date">Date of the ride</param>
        /// <param name="time">Time of the ride</param>
        /// <param name="startLocation">Start point of the ride</param>
        /// <param name="destination">Destination</param>
        /// <returns>Returs list of rides matching the conditions</returns>
        public async Task<List<Ride>> GetMatches(string userId,DateTime date, int time, string startLocation, string destination)
            { 
                var matches = new List<Ride>();
                List<OfferedRide> res =await _carPoolContext.OfferedRide.Include(or=>or.Locations).Include(or=>or.BookedRides).Where(rides => rides.UserId!=userId && rides.Date == date && rides.Time == time && (rides.Locations.Any(loc => loc.Location == startLocation) && rides.Locations.Any(loc => loc.Location == destination) && rides.AvailableSeats > 0)).ToListAsync();
                for(int i =0; i < res.Count; i++)
                {
                    var index = i;
                    var match = _mapperConfig.OfferedRideToRide().Map<Ride>(res[index]);
                    matches.Add(match);    
                }
            return matches.Where(rides => rides.Location.FindIndex(loc => loc == startLocation) < rides.Location.FindIndex(loc => loc == destination)).ToList();
            }


        /// <summary>
        /// Method to add ride for listing.It Takes user input as a instence of ride  class and adds it to the Rides list. 
        /// </summary>
        /// <param name="ride">Details of ride to be Added</param>
        /// <returns>Success or not response as bool</returns>
            public async Task<bool> OfferRide(Ride ride)
            {
                OfferedRide offeredRide = _mapperConfig.RideToOfferedRide().Map<OfferedRide>(ride);

                _carPoolContext.OfferedRide.Add(offeredRide);
                await _carPoolContext.SaveChangesAsync();
                int i = -1;
                foreach (var loc in ride.Location)
                {
                    ++i;
                    _carPoolContext.RideLocation.Add(new()
                    {
                        Location = loc,
                        SequenceNum = i,
                        RideId = ride.RideId

                    });
                }
                await _carPoolContext.SaveChangesAsync();
                return true;
            }


        /// <summary>
        /// Method to Get History of booked Rides.It Takes User Id And Compares It With RideTakenBy  of ride class
        /// and returns the list of matches as response.
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <returns>List of Rides that matches the querry</returns>
            public async Task<List<Ride>> GetBookedRideHistory(string userId)
            {
                var matches = new List<Ride>();
                List<OfferedRide> res =await _carPoolContext.OfferedRide.Include(or => or.Locations).Include(or => or.BookedRides).Where(rides => rides.BookedRides.Any(br=> br.UserId == userId)).ToListAsync();
                for (int i = 0; i < res.Count; i++)
                {
                    matches.Add(new Ride());
                    var index = i;
                    matches[index] = _mapperConfig.OfferedRideToRide().Map<Ride>(res[index]);
                  
                }
                return matches;
            }

            /// <summary>
            /// Methos to get history of offered rides .
            /// </summary>
            /// <param name="userId">Id of user</param>
            /// <returns> List of rides that matches the querry</returns>
            
            public async Task<List<Ride>> GetOfferedRideHistory(string userId)
            {
                var matches = new List<Ride>();
                List<OfferedRide> res = await _carPoolContext.OfferedRide.Include(or => or.Locations).Include(or => or.BookedRides).Where(rides => rides.UserId==userId).ToListAsync();
                for (int i = 0; i < res.Count; i++)
                {
                    matches.Add(new Ride());
                    var index = i;
                    matches[index] = _mapperConfig.OfferedRideToRide().Map<Ride>(res[index]);
                }
                return matches;
            }

        /// <summary>
        /// Method to Book a ride. It takes userEmail, seats and rideId as arguments. First checks the values for null than gets the ride being booked using 
        /// rideId and assigns userEmail to rideTakenBy field of the ride.
        /// </summary>
        /// <param name="userEmail">Email of User</param>
        /// <param name="seats">No of seats being booked</param>
        /// <param name="rideId">Id of the ride being booked</param>
        /// <returns>Success or not response as bool</returns>
            public async Task<bool> Booking(string userId , int seats , string rideId)
            {
                    OfferedRide match;
                    match =_carPoolContext.OfferedRide.FirstOrDefault(ride=> ride.RideId== rideId);
                    for(int i = 1; i <= seats; i++)
                    {
                        match.BookedRides.Add(new()
                        {
                            UserId=userId,
                            RideId=rideId,
                        });
                        match.AvailableSeats= match.AvailableSeats-1;
                    }
                    await _carPoolContext.SaveChangesAsync();
                    return true;
            }
    }
}

