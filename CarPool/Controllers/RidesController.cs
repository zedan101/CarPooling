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

        /// <summary>
        /// Constructor of RidesController
        /// </summary>
        /// <param name="ridesService">Instence of IRidesService interface</param>
        public RidesController(IRidesService ridesService)
        {
            _ridesService = ridesService;
        }


        /// <summary>
        /// Controller to recive data from frontend and pass it for processing to GetMatches method of RidesService.
        /// </summary>
        /// <param name="date">Date entered by user in frontend</param>
        /// <param name="time">Time entered by user in frontend</param>
        /// <param name="startLocation">Start Location entered by user in frontend</param>
        /// <param name="destination">Destination entered by user in frontend</param>
        /// <returns>Returs response from the GetMatches method of RidesService</returns>
        [HttpGet("GetRideMatches")]
        [Authorize]

        public IEnumerable<Ride> GetRideMatches(DateTime date, int time, string startLocation, string destination)
        {
            string userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _ridesService.GetMatches(userId,date, time, startLocation, destination).Result;
        }

        /// <summary>
        /// Controller to recive data from frontend and pass it for processing to OfferRide method of RidesService.
        /// </summary>
        /// <param name="ride">User Input as instence of Rides class</param>
        /// <returns>Returns response from OfferRide method of RidesService</returns>
        [HttpPost("PushRide")]
        [Authorize]

        public bool PushRides([FromBody] Ride ride)
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
                    ride.RideOfferedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    return _ridesService.OfferRide(ride).Result;
                    
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
        [HttpGet("GetBookedHistory")]
        [Authorize]

        public List<Ride> GetBookedHistory()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _ridesService.GetBookedRideHistory(userId).Result;
        }

        /// <summary>
        /// Controller to get userId from the token and pass it for processing to GetOfferedRideHistory method of RidesService.
        /// </summary>
        /// <returns>Returns response from GetOfferedRideHistory method of RidesService</returns>

        [HttpGet("GetOfferedHistory")]
        [Authorize]

        public List<Ride> GetOfferedHistory()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _ridesService.GetOfferedRideHistory(userId).Result;
        }

        /// <summary>
        /// Conroller to get no of seats, ride id from frontend and get the userEmail from token and pass it for processing to Booking method of RidesService.
        /// </summary>
        /// <param name="seats">UserInput of no of seats to book</param>
        /// <param name="rideId">Id of ride being booked</param>
        /// <returns>Returns the response from Booking method of RidesService</returns>
        [HttpPost("Booking")]
        [Authorize]

        public bool Booking(int seats , string rideId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null && seats != 0 && rideId != null)
            {
                return _ridesService.Booking(userId, seats, rideId).Result;
            }
            else
            {
                return false;
            }
        }
    }
}
