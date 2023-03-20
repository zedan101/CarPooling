using CarPool.Models;

namespace CarPool.Services.Interfaces
{
    public interface IUsersService
    {
        public User GetUsers(string userId);
        public Task<bool> PostUserDetails(User users);
        public Task<bool> ChangePassword(string userId, string newPass);
        public Task<bool> UpdateProfile(string userId, string? userName, string? profileImg);
        public Task<bool> DeleteProfile(string userId);
        public bool ValidateUser(string userEmail, string password);
        public bool ValidateEmail(string email);


    }
}
