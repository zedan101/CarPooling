using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class Enums
    {
        public enum Time
        {
            [Description("5am-9am")]
            _5amto9am,
            [Description("9am-12pm")]
            _9amto12pm,
            [Description("12pm-3pm")]
            _12amto3pm,
            [Description("3pm-6pm")]
            _3pmto6pm,
            [Description("6pm-9pm")]
            _6pmto9pm

        }
    }
}
