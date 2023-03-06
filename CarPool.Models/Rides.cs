using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    /// <summary>
    /// Model for storing data of rides.
    /// </summary>
    public class Rides
    {
        /// <summary>
        /// List of locations to store start point , stops and destination of the ride. 
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public List<string> Location = new List<string>();

        /// <summary>
        /// Date of the ride.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Time of the ride.
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Number of seats available.
        /// </summary>
        public string NumberOfSeatsAvailable { get; set; }

        /// <summary>
        /// Price per seat
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Id of the Ride(Not a user Input).
        /// </summary>
        public string RideId { get; set; }

        /// <summary>
        /// Email of the user offering ride.
        /// </summary>
        public string RideOfferedBy { get; set; }

        /// <summary>
        /// List of emails of users taking the ride.
        /// </summary>
        public List<string> RideTakenBy = new List<string>();

    }
}
