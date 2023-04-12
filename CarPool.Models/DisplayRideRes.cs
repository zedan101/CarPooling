using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class DisplayRideRes
    {
       public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public string RideId { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime Date { get; set; }
        public Enums.Time Time { get; set; }
        public double Price { get; set; }
        public int NumberOfSeatsAvailable { get; set; }
        public string? RideOfferedBy { get; set; }
    }
}
