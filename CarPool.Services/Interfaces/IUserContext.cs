using CarPool.Models;

namespace CarPool.Services.Interfaces
{
    public interface IUserContext
    {
        string UserId { get; }

        User LoggedInUser { get; }
    }
}
