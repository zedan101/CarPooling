using CarPool.Models;

namespace CarPool.Services.Interfaces
{
    public interface IUsersService
    {
        public User GetUsers(string userId);
        public Task<bool> PostUserDetails(User users);
        public bool ValidateUser(string userEmail, string password);
        public bool ValidateEmail(string email);


    }
}
