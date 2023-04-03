using Microsoft.AspNetCore.Mvc;
using CarPool.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using CarPool.Services.Interfaces;
using System.Security.Claims;
using Microsoft.Extensions.FileSystemGlobbing;
using AutoMapper;

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
        private readonly IMapper _mapper;
        private readonly IUsersService _userService;
        /// <summary>
        /// Constructor of RidesController
        /// </summary>
        /// <param name="ridesService">Instence of IRidesService interface</param>
        public RidesController(IRidesService ridesService,IMapper mapper, IUsersService userService) { 
            _ridesService = ridesService;
            _mapper = mapper;
            _userService = userService;
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
        public async Task<IEnumerable<RideHistoryRes>> RideMatches(MatchRideReq matchReq)
        {
            var rides = await _ridesService.MatchRides(matchReq);
            List<RideHistoryRes> ridesRes = new ();
            int i = 0;
            foreach (var ride in rides)
            {
                ridesRes.Add(new());
                ridesRes[i] = _mapper.Map<RideHistoryRes>(ride);
                var user = await _userService.GetUserDetail(ride.RideOfferedBy);
                ridesRes[i].ProfileImage = user.ProfileImage;
                ridesRes[i].UserName = user.UserName;
                //ridesRes[i] = _mapper.Map<RideHistoryRes>((await _userService.GetUserDetail(ride.RideOfferedBy)));
                i++;
            }
            return ridesRes;
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
                    ride.RideOfferedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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
        public async Task<IEnumerable<RideHistoryRes>> BookedHistory()
        {
            var rides = await _ridesService.BookedRideHistory();
            List<RideHistoryRes> ridesRes = new();
            int i = 0;
            foreach (var ride in rides)
            {
                ridesRes.Add(new());
                ridesRes[i] = _mapper.Map<RideHistoryRes>(ride);
                var user = await _userService.GetUserDetail(ride.RideOfferedBy);
                ridesRes[i].ProfileImage = user.ProfileImage;
                ridesRes[i].UserName= user.UserName;
               // ridesRes[i] = _mapper.Map<RideHistoryRes>((await _userService.GetUserDetail(ride.RideOfferedBy)));
                i++;
            }
            return ridesRes;
        }

        /// <summary>
        /// Controller to get userId from the token and pass it for processing to GetOfferedRideHistory method of RidesService.
        /// </summary>
        /// <returns>Returns response from GetOfferedRideHistory method of RidesService</returns>

        [HttpGet("OfferedHistory")]
        [Authorize]
        public async Task<IEnumerable<RideHistoryRes>> OfferedHistory()
        {
            var rides = (await _ridesService.OfferedRideHistory()).ToList();
            List<RideHistoryRes> ridesRes = new List<RideHistoryRes>();
            int i = 0;
            foreach (var ride in rides)
            {
                ridesRes.Add(new());
                ridesRes[i] = _mapper.Map<RideHistoryRes>(ride);
                var user = await _userService.GetUserDetail(ride.RideOfferedBy);
                ridesRes[i].ProfileImage = user.ProfileImage;
                ridesRes[i].UserName = user.UserName;
                //ridesRes[i] = _mapper.Map<RideHistoryRes>((await _userService.GetUserDetail(ride.RideOfferedBy)));
                i++;
            }
            return ridesRes;
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

            if (seats != 0 && rideId != null)
            {
                return await _ridesService.BookingRide(seats, rideId);
            }
            else
            {
                return false;
            }
        }

    }
}
