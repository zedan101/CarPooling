using AutoMapper;
using Carpool.Services.Data;
using CarPool.Models;
using CarPool.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarPool.Services
{
    public class UserContext:IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccess;
        private readonly CarPoolContext _carPoolContext;
        private readonly IMapper _mapper;
        public UserContext(IHttpContextAccessor httpContextAccess, CarPoolContext carPoolContext, IMapper mapper)
        {
            _httpContextAccess = httpContextAccess;
            _carPoolContext = carPoolContext;
            _mapper = mapper;
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
                return _mapper.Map<User>(_carPoolContext.User.First(user => user.UserId == UserId));
                 
            }
        }
    }
}
