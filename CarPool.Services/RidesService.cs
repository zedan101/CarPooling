using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.DataLayer.Models;
using CarPool.Models;
using CarPool.Services.Interfaces;
using CarpoolDataLayer;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarPool.Services
{
    public class RidesService : IRidesService
    {

        private readonly CarPoolContext _carPoolContext;
        public RidesService(CarPoolContext carPoolContext)
        {
            _carPoolContext = carPoolContext;
        }

        /// <summary>
        /// Method to get user input and match the input with available rides and send back the matched rides.
        /// </summary>
        /// <param name="date">Date of the ride</param>
        /// <param name="time">Time of the ride</param>
        /// <param name="startLocation">Start point of the ride</param>
        /// <param name="destination">Destination</param>
        /// <returns>Returs list of rides matching the conditions</returns>
        public async Task<List<Ride>> GetMatches(DateTime date, int time, string startLocation, string destination)
            {
                var matches = new List<Ride>();
                List<OfferedRide> res =await _carPoolContext.OfferedRide.Include(or=>or.Locations).Include(or=>or.BookedRides).Where(rides => rides.Date == date && rides.Time == time ).ToListAsync();
                var match = new Ride();
                for(int i =0; i < res.Count; i++)
                {
                    match.Location = res[i].Locations.OrderBy(loc=>loc.SequenceNum).Select(loc => loc.Location).ToList();
                    match.Date = res[i].Date;
                    match.Time = res[i].Time;
                    match.NumberOfSeatsAvailable= res[i].AvailableSeats;
                    match.Price= res[i].Price;
                    match.RideId= res[i].RideId;
                    match.RideOfferedBy = res[i].UserId;
                    match.RideTakenBy= res[i].BookedRides.Select(br=> br.UserId).ToList();
                    matches.Add(match);    
                }
                return matches.FindAll(ride => ride.Location.Any(loc => loc == startLocation) && ride.Location.Any(loc => loc == destination) && ride.Location.FindIndex(loc => loc == startLocation) < ride.Location.FindIndex(loc => loc == destination && ride.NumberOfSeatsAvailable > 0)).ToList();

            }


        /// <summary>
        /// Method to add ride for listing.It Takes user input as a instence of ride  class and adds it to the Rides list. 
        /// </summary>
        /// <param name="ride">Details of ride to be Added</param>
        /// <returns>Success or not response as bool</returns>
            public async Task<bool> OfferRide(Ride ride)
            {
                _carPoolContext.OfferedRide.Add(new() { 
                    RideId = ride.RideId,
                    Date = ride.Date,
                    Time = ride.Time,
                    AvailableSeats = ride.NumberOfSeatsAvailable,
                    Price = ride.Price,
                    UserId = ride.RideOfferedBy,
                });
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
                    matches.Add(new()
                    {
                        Location = res[i].Locations.OrderBy(loc => loc.SequenceNum).Select(loc => loc.Location).ToList(),
                        Date = res[i].Date,
                        Time = res[i].Time,
                        NumberOfSeatsAvailable = res[i].AvailableSeats,
                        Price = res[i].Price,
                        RideId = res[i].RideId,
                        RideOfferedBy = res[i].UserId,
                        RideTakenBy = res[i].BookedRides.Select(br => br.UserId).ToList()

                    });
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
                    matches.Add(new() {
                       Location = res[i].Locations.OrderBy(loc => loc.SequenceNum).Select(loc => loc.Location).ToList(),
                       Date = res[i].Date,
                        Time = res[i].Time,
                        NumberOfSeatsAvailable = res[i].AvailableSeats,
                        Price = res[i].Price,
                        RideId = res[i].RideId,
                        RideOfferedBy = res[i].UserId,
                        RideTakenBy = res[i].BookedRides.Select(br => br.UserId).ToList()
                
                    });
                    
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

