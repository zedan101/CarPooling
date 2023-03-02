using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class Rides
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public List<string> Location = new List<string>();
        public string Date { get; set; }
        public string Time { get; set; }
        public string NumberOfSeatsAvailable { get; set; }
        public string Price { get; set; }
        public string RideId { get; set; }
        public string RideOfferedBy { get; set; }
        public string RideTakenBy { get; set; }
    }
}
