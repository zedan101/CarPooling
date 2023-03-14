using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarPool.Models
{
    /// <summary>
    /// Model for storing data of rides.
    /// </summary>
    public class Ride
    {
        /// <summary>
        /// List of locations to store start point , stops and destination of the ride. 
        /// </summary>
        [JsonInclude]
        public List<string> Location = new();

        /// <summary>
        /// Date of the ride.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Time of the ride.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Number of seats available.
        /// </summary>
        public int NumberOfSeatsAvailable { get; set; }

        /// <summary>
        /// Price per seat
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Id of the Ride(Not a user Input).
        /// </summary>
        public string RideId { get; set; }

        /// <summary>
        /// id of the user offering ride.
        /// </summary>
        public string? RideOfferedBy { get; set; }

        /// <summary>
        /// List of id of users taking the ride.
        /// </summary>
        [JsonInclude]
        public List<string>? RideTakenBy = new();

    }
}
