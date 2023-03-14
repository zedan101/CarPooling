using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarPool.Models
{
    /// <summary>
    /// Class to store Users data.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Email of the user.
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Private mmember password
        /// </summary>
        private string _password;

        /// <summary>
        /// Profile image of the user
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Name of the user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Id of the user
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Password property of _password member
        /// </summary>
        [JsonIgnore]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
    }
}
