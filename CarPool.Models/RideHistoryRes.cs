using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class RideHistoryRes
    {
       public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public string RideId { get; set; }
        public List<string> Location { get; set; }
        public DateTime Date { get; set; }
        public Enums.Time Time { get; set; }
        public double Price { get; set; }
        public int NumberOfSeatsAvailable { get; set; }
        public string? RideOfferedBy { get; set; }
    }
}
