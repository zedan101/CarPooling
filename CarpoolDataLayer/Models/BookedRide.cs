using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.DataLayer.Models
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
    }
}
