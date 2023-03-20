using CarPool.Models;
using CarPool.Services.Interfaces;
using Carpool.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public class UsersService:IUsersService
    {

        private readonly CarPoolContext _carPoolContext;
        public UsersService(CarPoolContext carPoolContext)
        {
            _carPoolContext = carPoolContext;
        }

        /// <summary>
        /// Method to get user details. 
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>instence of Users class contains data of user whoes id matches the argument </returns>
        public User GetUsers(string userId)
        {
            var user = _carPoolContext.User.FirstOrDefault(user => user.UserId == userId);
            return new User() { UserEmail = user.UserEmail, UserName = user.Name,Password =user.Password , ProfileImage = user.ProfileImage , UserId=user.UserId};
        }

        /// <summary>
        /// Method to post user details when he does sign up.
        /// </summary>
        /// <param name="users">User Details as instence of Users Class</param>
        /// <returns>Success or not response as bool</returns>
        public async Task<bool> PostUserDetails(User users)
        {
            _carPoolContext.User.Add(new DataLayer.Models.UserEntity()
            {
                UserEmail= users.UserEmail,
                Name = users.UserName,
                UserId= users.UserId,
                Password= users.Password,
                ProfileImage= users.ProfileImage,
            });
            await _carPoolContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Method to Change password of the user.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="newPass">new password</param>
        /// <returns>returns success or fail reponse as bool value</returns>
        public async Task<bool> ChangePassword(string userId,string newPass)
        {
            _carPoolContext.User.First(user=> user.UserId== userId).Password = newPass;
            await _carPoolContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Method to update profile information of the user. 
        /// </summary>
        /// <param name="userId"> ID of user</param>
        /// <param name="userName">New user name</param>
        /// <param name="profile">new profile image</param>
        /// <returns></returns>
        public async Task<bool> UpdateProfile(string userId , string? userName, string? profile)
        {
            if(userName!=null || profile!=null)
            {
                _carPoolContext.User.First(user=>user.UserId == userId).ProfileImage = profile;
                _carPoolContext.User.First(user => user.UserId == userId).Name = userName;
                await _carPoolContext.SaveChangesAsync();
                return true;
            }
            else if (userName != null)
            {
                _carPoolContext.User.First(user=> user.UserId== userId).Name = userName;
                await _carPoolContext.SaveChangesAsync();
                return true; 
            }
            else if (profile != null)
            {
                _carPoolContext.User.First(user => user.UserId == userId).ProfileImage = profile;
                await _carPoolContext.SaveChangesAsync(); 
                return true;
            }
            else
            {
                return false; 
            }
        }

        /// <summary>
        /// Method to delete profile from database;
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <returns>response success or fail as bool value</returns>
        public async Task<bool> DeleteProfile(string userId)
        {
            if (userId != null)
            {
                var usr = _carPoolContext.User.FirstOrDefault(user => user.UserId == userId);
                if (usr != null)
                {
                    _carPoolContext.User.Remove(usr);
                    await _carPoolContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Method to validate the email and password of the user while login
        /// </summary>
        /// <param name="userEmail">Email of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>Valid or not response as bool</returns>
        public bool ValidateUser(string userEmail, string password)
        {
            if(userEmail == null || password == null)
            {
                return false;
            }
            else
            {
                return _carPoolContext.User.Any(user => user.UserEmail == userEmail && user.Password == password);
            }
        }

        /// <summary>
        /// Method to validate the user Email at SignUp . So that user cannot create two ids with same user email. 
        /// </summary>
        /// <param name="email">Email entered by user at SignUp</param>
        /// <returns>valid or not response as bool</returns>
        public bool ValidateEmail(string email)
        {
            return _carPoolContext.User.Any(user => user.UserEmail == email);
        }
    }
}
