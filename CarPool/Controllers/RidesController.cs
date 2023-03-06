using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarPool.Models;
using System.Collections.Generic;
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

        public IEnumerable<Rides> GetRideMatches(string date, string time, string startLocation, string destination)
        {
           return _ridesService.GetMatches(date, time, startLocation, destination);
        }

        /// <summary>
        /// Controller to recive data from frontend and pass it for processing to OfferRide method of RidesService.
        /// </summary>
        /// <param name="ride">User Input as instence of Rides class</param>
        /// <returns>Returns response from OfferRide method of RidesService</returns>
        [HttpPost("PushRide")]
        [Authorize]

        public bool PushRides([FromBody] Rides ride)
        {
            try
            {
                if(ride== null)
                {
                    return false;
                }
                else
                {
                    return _ridesService.OfferRide(ride);
                    
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Controller to get userEmail from the token and pass it for processing to GetRideHistory method of RidesService.
        /// </summary>
        /// <returns>Returns response from GetRideHistory method of RidesService</returns>
        [HttpGet("GetHistory")]
        [Authorize]

        public List<Rides> GetHistory()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _ridesService.GetRideHistory(userId);
        }

        /// <summary>
        /// Conroller to get no of seats, ride id from frontend and get the userEmail from token and pass it for processing to Booking method of RidesService.
        /// </summary>
        /// <param name="seats">UserInput of no of seats to book</param>
        /// <param name="rideId">Id of ride being booked</param>
        /// <returns>Returns the response from Booking method of RidesService</returns>
        [HttpPut("Booking")]
        [Authorize]

        public bool Booking(int seats , string rideId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _ridesService.Booking(userId, seats, rideId);
        }
    }
}
