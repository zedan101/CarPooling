using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarPool.Models;
using CarPool.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace CarPool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RidesController : ControllerBase
    {
        private readonly IRidesService _ridesService;
        public RidesController(IRidesService ridesService)
        {
            _ridesService = ridesService;
        }

        

        [HttpGet]
        [Route("GetRideMatches")]
        [Authorize]

        public string GetRideMatches(string date, string time, string startLocation, string destination)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Include
            };
            return JsonConvert.SerializeObject(_ridesService.GetMatches(date, time, startLocation, destination), Formatting.Indented, serializerSettings);
         
          //  return _ridesService.GetMatches(date, time, startLocation, destination);


        }

        [HttpPost]
        [Route("PushRide")]
        [Authorize]

        public bool PushRides(Rides ride)
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

        [HttpGet]
        [Route("GetHistory")]
        [Authorize]

        public List<Rides> GetHistory(string userId)
        {
             return _ridesService.GetRideHistory(userId);
        }
    }
}
