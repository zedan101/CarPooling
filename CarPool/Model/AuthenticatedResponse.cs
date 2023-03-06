using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Model
{
    /// <summary>
    /// Model for response from authentication
    /// </summary>
    public class AuthenticatedResponse
    {
        /// <summary>
        /// Authentication token
        /// </summary>
        public string? Token { get; set; }
    }
}
