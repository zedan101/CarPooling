using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class MatchRideReq
    {
        public DateTime Date { get; set; }
        public Enums.Time Time { get; set; }

        public string StartLocation { get; set; }

        public string EndLocation { get; set; }

    }
}
