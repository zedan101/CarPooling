using CarPool.Models;
using CarPool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public class UsersService:IUsersService
    {

        /// <summary>
        /// Method to get user details. 
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>instence of Users class contains data of user whoes id matches the argument </returns>
        public Users GetUsers(string userEmail)
        {
            return GlobalStorage.Users.FirstOrDefault(user => user.UserEmail == userEmail);
        }

        /// <summary>
        /// Get all The users info
        /// </summary>
        /// <returns>List of all the users </returns>
        public List<Users> GetUsers()
        {
            return GlobalStorage.Users;
        }

        /// <summary>
        /// Method to post user details when he does sign up.
        /// </summary>
        /// <param name="users">User Details as instence of Users Class</param>
        /// <returns>Success or not response as bool</returns>
        public bool PostUserDetails(Users users)
        {
            GlobalStorage.Users.Add(users);
            return true;
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
                return GlobalStorage.Users.Any(user => user.UserEmail == userEmail && user.Password == password);
            }
        }

        /// <summary>
        /// Method to validate the user Email at SignUp . So that user cannot create two ids with same user email. 
        /// </summary>
        /// <param name="email">Email entered by user at SignUp</param>
        /// <returns>valid or not response as bool</returns>
        public bool ValidateEmail(string email)
        {
            return GlobalStorage.Users.Any(user => user.UserEmail == email);
        }
    }
}
