using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class Users
    {
        public string UserEmail { get; set; }
        private string _password;
        public string ProfileImage { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }

        [JsonIgnore]
        public string Password
        {
            get; set;
        }
    }
}
