﻿using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Interfaces
{
    public interface IUserContext
    {
        string UserId { get; }
       // public string GetLoggedInUserId();
    }
}
