using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Models
{
    public class UserEntity
    {
        [Key]
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OfferedRide> OfferedRides { get; set; }

        public virtual ICollection<BookedRide> BookedRides { get; set; }

    }
}
