using CarPool.Models;

namespace CarPool.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<User> GetUserDetail(string userId);
        public Task<int> SubmitUserDetails(User users);
        public Task<int> ChangePassword(string userId, string newPass);
        public Task<bool> UpdateUserProfile(string userId, string? userName, string? profileImg);
        public Task<int> DeleteUser(string userId);
        public Task<bool> IsEmailAlreadyRegistered(string email);


    }
}
