using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPool.DataLayer.Models
{
    public class RideLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerialNo { get; set; }
        public string? Location { get; set; }
        public int SequenceNum { get; set; }
        [ForeignKey("OfferRide")]
        public string RideId { get; set; }
        public virtual OfferedRide? OfferRide { get; set; }
    }
}
