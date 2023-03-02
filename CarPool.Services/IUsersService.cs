using CarPool.Models;

namespace CarPool.Interfaces
{
    public interface IUsersService
    {
        public Users GetUsers(string userId);
        public List<Users> GetUsers();
        public bool PostUserDetails(Users users);
        public bool ValidateUser(string userEmail, string password);
        public bool ValidateEmail(string email);


    }
}
