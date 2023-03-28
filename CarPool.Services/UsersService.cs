using CarPool.Models;
using CarPool.Services.Interfaces;
using Carpool.DataLayer;
using CarPool.DataLayer.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CarPool.Services
{
    public class UsersService:IUsersService
    {

        private readonly CarPoolContext _carPoolContext;
        private readonly IMapper _mapper;
        public UsersService(CarPoolContext carPoolContext, IMapper mapper)
        {
            _carPoolContext = carPoolContext;
            _mapper = mapper;
        }
        /// <summary>
        /// Method to get user details. 
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>instence of Users class contains data of user whoes id matches the argument </returns>
        public async Task<User> GetUserDetail(string userId)
        {
            var user =_mapper.Map<User>(await _carPoolContext.User.FirstAsync(user => user.UserId == userId));
            return user;
        }

        /// <summary>
        /// Method to post user details when he does sign up.
        /// </summary>
        /// <param name="users">User Details as instence of Users Class</param>
        /// <returns>Success or not response as bool</returns>
        public async Task<int> SubmitUserDetails(User users)
        {
            UserEntity user = _mapper.Map<UserEntity>(users);
            _carPoolContext.User.Add(user);
            var res = await _carPoolContext.SaveChangesAsync();
            return res;
        }

        /// <summary>
        /// Method to Change password of the user.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="newPass">new password</param>
        /// <returns>returns success or fail reponse as bool value</returns>
        public async Task<int> ChangePassword(string userId,string newPass)
        {
            _carPoolContext.User.First(user=> user.UserId== userId).Password = newPass;
            var res = await _carPoolContext.SaveChangesAsync();
            return res;
        }

        /// <summary>
        /// Method to update profile information of the user. 
        /// </summary>
        /// <param name="userId"> ID of user</param>
        /// <param name="userName">New user name</param>
        /// <param name="profile">new profile image</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserProfile(string userId , string? userName, string? profile)
        {
            if (userName!=null || profile!=null)
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
        public async Task<int> DeleteUser(string userId)
        {
                var usr =await _carPoolContext.User.FirstAsync(user => user.UserId == userId);
                _carPoolContext.User.Remove(usr);
                var res = await _carPoolContext.SaveChangesAsync();
                return res;
                
             
        }


        /// <summary>
        /// Method to validate the user Email at SignUp . So that user cannot create two ids with same user email. 
        /// </summary>
        /// <param name="email">Email entered by user at SignUp</param>
        /// <returns>valid or not response as bool</returns>
        public async Task<bool> IsEmailAlreadyRegistered(string email)
        {
             var res =   await _carPoolContext.User.FirstAsync(user => user.UserEmail == email);
            return res!= null;
        }
    }
}
