using CarPool.Services.Data.Models;


namespace CarPool.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> ValidateUser(string userEmail, string password);
        public Task<UserEntity> GetClaimDataForUserIdentification(string userEmail);

        public Task<bool> ChangePassword(string newPass);
    }
}
