using CarPool.Services.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPool.Services.Data.Models
{
    public class RideLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerialNo { get; set; }
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public virtual Locations Location { get; set; }
        public int SequenceNum { get; set; }
        [ForeignKey("OfferRide")]
        public string RideId { get; set; }
        public virtual OfferedRide? OfferRide { get; set; }
    }
}
