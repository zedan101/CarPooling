using CarPool.Models;

namespace CarPool.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<User> GetUserDetail(string userId);
        public Task<bool> SubmitUserDetails(User users);
        public Task<bool> UpdateUserProfile(string? userName, string? profileImg);
        public Task<bool> DeleteUser();
        public Task<bool> IsEmailAlreadyRegistered(string email);


    }
}
