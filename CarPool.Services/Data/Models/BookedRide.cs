using CarPool.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Models
{
    public class BookedRide
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SlNo { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual UserEntity? User { get; set; }

        [ForeignKey("OfferedRide")]
        public string RideId { get; set; }

        public virtual OfferedRide? OfferedRide { get; set; }
        [ForeignKey("StartLocation")]
        public int StartLocationId { get; set; }
        public virtual Locations StartLocation { get; set; }
        [ForeignKey("EndLocation")]
        public int EndLocationId { get; set;}
        public virtual Locations EndLocation { get; set; }
        public DateTime BookedOn { get; set; }
    }
}
