using AutoMapper;
using Carpool.DataLayer;
using CarPool.Models;
using CarPool.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public class UserContext:IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccess;
        public UserContext(IHttpContextAccessor httpContextAccess)
        {
            _httpContextAccess = httpContextAccess;
        }

        public string UserId
        {
            get
            {
                return _httpContextAccess.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }
    }
}
