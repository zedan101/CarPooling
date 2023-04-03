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

        private readonly IUserContext _userContext;
        private readonly CarPoolContext _carPoolContext;
        public AuthService(CarPoolContext carPoolContext, IUserContext userContext)
        {
            _carPoolContext = carPoolContext;
            _userContext = userContext;
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

        /// <summary>
        /// Method to Change password of the user.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="newPass">new password</param>
        /// <returns>returns success or fail reponse as bool value</returns>
        public async Task<bool> ChangePassword( string newPass)
        {
            (await _carPoolContext.User.FirstAsync(user => user.UserId == _userContext.UserId)).Password = newPass;
            var res = await _carPoolContext.SaveChangesAsync();
            return res > 0;
        }
    }
}
