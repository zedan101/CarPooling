using AutoMapper;
using Carpool.DataLayer;
using CarPool.DataLayer.Models;
using CarPool.Models;
using CarPool.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarPool.Services
{
    public class AuthService : IAuthService
    {

        private readonly IHttpContextAccessor _httpContextAccess;
        private readonly CarPoolContext _carPoolContext;
        public AuthService(CarPoolContext carPoolContext, IHttpContextAccessor httpContextAccess)
        {
            _carPoolContext = carPoolContext;
            _httpContextAccess = httpContextAccess;
        }


        /// <summary>
        /// Method to validate the email and password of the user while login
        /// </summary>
        /// <param name="userEmail">Email of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>Valid or not response as bool</returns>
        public async Task<bool> ValidateUser(string userEmail, string password)
        {
            var res = await _carPoolContext.User.FirstAsync(user => user.UserEmail == userEmail && user.Password == password);
            return res != null;
        }

       public async Task<UserEntity> GetClaimDataForUserIdentification(string userEmail)
        {
           return await _carPoolContext.User.FirstAsync(user=>user.UserEmail==userEmail);
                
        }

        public string GetUserIdByToken()
        {
            string userId = _httpContextAccess.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
    }
}
