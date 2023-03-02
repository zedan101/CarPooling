using CarPool.Interfaces;
using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public class UsersService:IUsersService
    {
        public Users GetUsers(string userId)
        {
            return GlobalStorage.Users?.FirstOrDefault(user => user.UserId == userId);
        }

        public List<Users> GetUsers()
        {
            return GlobalStorage.Users;
        }

        public bool PostUserDetails(Users users)
        {
            GlobalStorage.Users.Add(users);
            return true;
        }

        public bool ValidateUser(string userEmail, string password)
        {
            return GlobalStorage.Users.Any(user => user.UserEmail == userEmail && user.Password == password);
        }

        public bool ValidateEmail(string email)
        {
            return GlobalStorage.Users.Any(user => user.UserEmail == email);
        }
    }
}
