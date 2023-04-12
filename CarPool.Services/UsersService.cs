using CarPool.Models;
using CarPool.Services.Interfaces;
using Carpool.Services.Data;
using CarPool.Services.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CarPool.Services
{
    public class UsersService:IUsersService
    {

        private readonly CarPoolContext _carPoolContext;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        public UsersService(CarPoolContext carPoolContext, IMapper mapper , IUserContext userContext)
        {
            _carPoolContext = carPoolContext;
            _mapper = mapper;
            _userContext = userContext;
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
        public async Task<bool> SubmitUserDetails(User users)
        {
            UserEntity user = _mapper.Map<UserEntity>(users);
            await _carPoolContext.User.AddAsync(user);
            var res = await _carPoolContext.SaveChangesAsync();
            return res>0;
        }

       

        /// <summary>
        /// Method to update profile information of the user. 
        /// </summary>
        /// <param name="userId"> ID of user</param>
        /// <param name="userName">New user name</param>
        /// <param name="profile">new profile image</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserProfile( string userName, string profile)
        {
            var x = await _carPoolContext.User.FirstAsync(user => user.UserId == _userContext.UserId);
            x.Name= userName == x.Name ? x.Name : userName;
            x.ProfileImage = profile == x.ProfileImage ? x.ProfileImage : profile;
            var res = await _carPoolContext.SaveChangesAsync();
                return res > 0;
            
        }

        /// <summary>
        /// Method to delete profile from database;
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <returns>response success or fail as bool value</returns>
        public async Task<bool> DeleteUser()
        {
                var usr =await _carPoolContext.User.FirstAsync(user => user.UserId == _userContext.UserId);
                _carPoolContext.User.Remove(usr);
                var res = await _carPoolContext.SaveChangesAsync();
                return res>0;  
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
