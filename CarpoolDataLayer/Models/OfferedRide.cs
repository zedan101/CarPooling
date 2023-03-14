using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.DataLayer.Models
{
    public class OfferedRide
    {
        [Key]
        public string RideId { get; set; }
        public DateTime Date { get; set; }
        public int Time { get; set; }
        public int AvailableSeats { get; set; }
        public double Price { get; set; }

        [ForeignKey("UserEntity")]
        public string UserId { get; set; }
        public virtual UserEntity UserEntity { get; set; }

        public virtual ICollection<RideLocation> Locations { get; set; }

        public virtual ICollection<BookedRide> BookedRides { get; set; }

    }
}
