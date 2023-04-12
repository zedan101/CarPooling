
using AutoMapper;
using Carpool.Services.Data;
using CarPool.Services.Data.Models;
using CarPool.Models;
using CarPool.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing;

namespace CarPool.Services
{
    public class RidesService : IRidesService
    {

        private readonly CarPoolContext _carPoolContext;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        public RidesService(CarPoolContext carPoolContext, IMapper mapper, IUserContext userContext)
        {
            _carPoolContext = carPoolContext;
            _mapper = mapper;
            _userContext = userContext;
        }

        /// <summary>
        /// Method to get user input and match the input with available rides and send back the matched rides.
        /// </summary>
        /// <param name="date">Date of the ride</param>
        /// <param name="time">Time of the ride</param>
        /// <param name="startLocation">Start point of the ride</param>
        /// <param name="destination">Destination</param>
        /// <returns>Returs list of rides matching the conditions</returns>
        public async Task<IEnumerable<Ride>> MatchRides(MatchRideReq matchReq)
            { 
                IEnumerable<OfferedRide> res =(await _carPoolContext.OfferedRide.Include(or=>or.Locations)
                                            .Include(or=>or.BookedRides)
                                            .Where(rides => rides.UserId!= _userContext.UserId && rides.Date == matchReq.Date && rides.Time == (int)matchReq.Time
                                            && (rides.Locations.Any(loc => loc.LocationId == _carPoolContext.Locations.First(loc => loc.LocationName == matchReq.StartLocation).LocationId)
                                            && rides.Locations.Any(loc => loc.LocationId == _carPoolContext.Locations.First(loc => loc.LocationName == matchReq.EndLocation).LocationId)
                                            && rides.AvailableSeats > 0)).ToListAsync()).Where(rides => 
                                            rides.Locations.First(loc => loc.LocationId == _carPoolContext.Locations.First(loc => loc.LocationName == matchReq.StartLocation).LocationId).SequenceNum
                                            < rides.Locations.First(loc => loc.LocationId == _carPoolContext.Locations.First(loc => loc.LocationName == matchReq.EndLocation).LocationId).SequenceNum).ToList();
                
                var matches = _mapper.Map<IEnumerable<Ride>>(res);
            
                return matches;
            }


        /// <summary>
        /// Method to add ride for listing.It Takes user input as a instence of ride  class and adds it to the Rides list. 
        /// </summary>
        /// <param name="ride">Details of ride to be Added</param>
        /// <returns>Success or not response as bool</returns>
            public async Task<bool> OfferRide(Ride ride)
            {
                OfferedRide offeredRide = _mapper.Map<OfferedRide>(ride);
                offeredRide.CreatedOn= DateTime.Now;
                _carPoolContext.OfferedRide.Add(offeredRide);
                await _carPoolContext.SaveChangesAsync();
                int i = -1;
                foreach (var loc in ride.Location)
                {
                    ++i;
                    if (_carPoolContext.Locations.Any(l => l.LocationName == loc))
                    {
                        _carPoolContext.RideLocation.Add(new()
                        {
                            LocationId = _carPoolContext.Locations.First(l => l.LocationName == loc).LocationId,
                            SequenceNum = i,
                            RideId = ride.RideId

                        });
                    }
                    else
                    {
                        _carPoolContext.Locations.Add(new() { LocationName = loc, });
                        await _carPoolContext.SaveChangesAsync();
                        _carPoolContext.RideLocation.Add(new()
                        {
                            LocationId = _carPoolContext.Locations.First(l => l.LocationName == loc).LocationId,
                            SequenceNum = i,
                            RideId = ride.RideId

                        });
                    }     
                }
                var res = await _carPoolContext.SaveChangesAsync();

                return res>0;
            }


        /// <summary>
        /// Method to Get History of booked Rides.It Takes User Id And Compares It With RideTakenBy  of ride class
        /// and returns the list of matches as response.
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <returns>List of Rides that matches the querry</returns>
            public async Task<IEnumerable<Ride>>BookedRideHistory()
            {
                IEnumerable<OfferedRide> res =await _carPoolContext.OfferedRide.Include(or => or.Locations)
                                        .Include(or => or.BookedRides)
                                        .Where(rides => rides.BookedRides.Any(br=> br.UserId == _userContext.UserId)).ToListAsync();
                var matches = _mapper.Map<IEnumerable<Ride>>(res);
                return matches;
            }

            /// <summary>
            /// Methos to get history of offered rides .
            /// </summary>
            /// <param name="userId">Id of user</param>
            /// <returns> List of rides that matches the querry</returns>
            
            public async Task<IEnumerable<Ride>> OfferedRideHistory()
            {
                IEnumerable<OfferedRide> res = await _carPoolContext.OfferedRide.Include(or => or.Locations)
                                                .Include(or => or.BookedRides)
                                                .Where(rides => rides.UserId==_userContext.UserId).ToListAsync();
                var matches = _mapper.Map<IEnumerable<Ride>>(res);   
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
            public async Task<bool> BookingRide( int seats , string rideId,string startLocation,string endLocation)
            {
                    OfferedRide match;
                    match =_carPoolContext.OfferedRide.FirstOrDefault(ride=> ride.RideId== rideId);
                    if(!_carPoolContext.Locations.Any(l => l.LocationName == startLocation))
                    {
                        _carPoolContext.Locations.Add(new() { LocationName = startLocation, });
                    }
                    else if(!_carPoolContext.Locations.Any(l => l.LocationName == endLocation))
                    {
                        _carPoolContext.Locations.Add(new() { LocationName = endLocation, });
                    }
                    for(int i = 1; i <= seats; i++)
                    {
                        match.BookedRides.Add(new()
                        {
                            UserId=_userContext.UserId,
                            RideId=rideId,
                            StartLocationId= _carPoolContext.Locations.First(l => l.LocationName == startLocation).LocationId,
                            EndLocationId= _carPoolContext.Locations.First(l=> l.LocationName==endLocation).LocationId,
                            BookedOn = DateTime.Now
                        });
                        match.AvailableSeats--;
                    }
                    await _carPoolContext.SaveChangesAsync();
                    return true;
            }

            public async Task<BookedRide> GetBookingInfo(string userId,string rideId)
            {
                    return await _carPoolContext.BookedRide.FirstAsync(bkr => bkr.UserId == userId && bkr.RideId == rideId);
            }
            public async Task<string> GetLocationById(int locationId)
            {
                return (await _carPoolContext.Locations.FirstAsync(loc=>loc.LocationId== locationId)).LocationName;
            }
    }
}

