using Microsoft.AspNetCore.Mvc;
using CarPool.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using CarPool.Services.Interfaces;
using System.Security.Claims;

namespace CarPool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RidesController : ControllerBase
    {
        /// <summary>
        /// Private Member of RidesController Class (Used for Dependency Injection)
        /// </summary>
        private readonly IRidesService _ridesService;
        private readonly IAuthService _authService;

        /// <summary>
        /// Constructor of RidesController
        /// </summary>
        /// <param name="ridesService">Instence of IRidesService interface</param>
        public RidesController(IRidesService ridesService, IAuthService authService)
        {
            _ridesService = ridesService;
            _authService = authService;
        }


        /// <summary>
        /// Controller to recive data from frontend and pass it for processing to GetMatches method of RidesService.
        /// </summary>
        /// <param name="date">Date entered by user in frontend</param>
        /// <param name="time">Time entered by user in frontend</param>
        /// <param name="startLocation">Start Location entered by user in frontend</param>
        /// <param name="destination">Destination entered by user in frontend</param>
        /// <returns>Returs response from the GetMatches method of RidesService</returns>
        [HttpGet("RideMatches")]
        [Authorize]

        public async Task<IEnumerable<Ride>> RideMatches(DateTime date, int time, string startLocation, string destination)
        {
            
            return await _ridesService.MatchRides(_authService.GetUserIdByToken(),date, time, startLocation, destination);
        }

        /// <summary>
        /// Controller to recive data from frontend and pass it for processing to OfferRide method of RidesService.
        /// </summary>
        /// <param name="ride">User Input as instence of Rides class</param>
        /// <returns>Returns response from OfferRide method of RidesService</returns>
        [HttpPost("OfferARide")]
        [Authorize]

        public async Task<bool> OfferARide([FromBody] Ride ride)
        {
            try
            {
                if(ride== null)
                {
                    return false;
                }
                else
                {
                    ride.RideId="ride"+ DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    ride.RideOfferedBy = _authService.GetUserIdByToken();
                    return await _ridesService.OfferRide(ride);
                    
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Controller to get userId from the token and pass it for processing to GetBookedRideHistory method of RidesService.
        /// </summary>
        /// <returns>Returns response from GetBookedRideHistory method of RidesService</returns>
        [HttpGet("BookedHistory")]
        [Authorize]

        public async Task<List<Ride>> BookedHistory()
        {
            return await _ridesService.BookedRideHistory(_authService.GetUserIdByToken());
        }

        /// <summary>
        /// Controller to get userId from the token and pass it for processing to GetOfferedRideHistory method of RidesService.
        /// </summary>
        /// <returns>Returns response from GetOfferedRideHistory method of RidesService</returns>

        [HttpGet("OfferedHistory")]
        [Authorize]

        public async Task<List<Ride>> OfferedHistory()
        {
            return await _ridesService.OfferedRideHistory(_authService.GetUserIdByToken());
        }

        /// <summary>
        /// Conroller to get no of seats, ride id from frontend and get the userEmail from token and pass it for processing to Booking method of RidesService.
        /// </summary>
        /// <param name="seats">UserInput of no of seats to book</param>
        /// <param name="rideId">Id of ride being booked</param>
        /// <returns>Returns the response from Booking method of RidesService</returns>
        [HttpPost("Booking")]
        [Authorize]

        public async Task<bool> Booking(int seats , string rideId)
        {

            if (_authService.GetUserIdByToken() != null && seats != 0 && rideId != null)
            {
                return await _ridesService.BookingRide(_authService.GetUserIdByToken(), seats, rideId);
            }
            else
            {
                return false;
            }
        }

    }
}
