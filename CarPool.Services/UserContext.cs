using AutoMapper;
using Carpool.DataLayer;
using CarPool.Models;
using CarPool.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CarPool.Services
{
    public class UserContext:IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccess;
        private readonly IUsersService _userService;
        public UserContext(IHttpContextAccessor httpContextAccess, IUsersService userService)
        {
            _httpContextAccess = httpContextAccess;
            _userService = userService;
        }

        public string UserId
        {
            get
            {
                return _httpContextAccess.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }

        public User LoggedInUser 
        {
            get 
            { 
                return _userService.GetUserDetail(UserId).GetAwaiter().GetResult();
            }
        }
    }
}
