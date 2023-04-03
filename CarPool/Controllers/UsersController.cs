using Microsoft.AspNetCore.Mvc;
using CarPool.Models;
using Microsoft.AspNetCore.Authorization;
using CarPool.Services.Interfaces;
using System.Security.Claims;

namespace CarPool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Private Member of UsersController Class (Used for Dependency Injection)
        /// </summary>
        private readonly IUsersService _usersService;

        /// <summary>
        /// Constructor of UsersController. 
        /// </summary>
        /// <param name="usersService">Instence of IUsersService interface</param>
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Controller gets the userEmail through token and passes it as an argument to the GetUsers Method of Users Service.
        /// </summary>
        /// <returns>Returns the data from GetUsers Method of Users Service</returns>
        [HttpGet("GetUserDetails")]
        [Authorize]

        public async Task<User> LoggedUserDetails()
        {
            return await _usersService.GetUserDetail(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        /// <summary>
        /// Controller to return response from the GetUsers method of Users Service.
        /// </summary>
        /// <returns>Returns List of Users returned by GetUsers method of Users Service</returns>
        [HttpGet("GetUsers")]
        [Authorize]

        public async Task<User> UserDetailForRideHistory(string userId)
        {
            return await _usersService.GetUserDetail(userId);
        }



        /// <summary>
        /// Controller to return response from UpdateProfile method of Users Service.
        /// </summary>
        /// <param name="userName">new userName Value</param>
        /// <param name="profileImage">new Profile Image value</param>
        /// <returns>returns response as bool</returns>
        [HttpPatch("UpdateProfile")]
        [Authorize]
        public async Task<bool> UpdateProfile([FromBody]User user)
        {
           
            return await _usersService.UpdateUserProfile( user.UserName, user.ProfileImage);
        }

        /// <summary>
        /// Controller to return response from Delete Profile method of Users Service.
        /// </summary>
        /// <returns>returns response as bool</returns>
        [HttpDelete("DeleteProfile")]
        [Authorize]
        public async Task<bool> RemoveUserAccount()
        {
            
            return await _usersService.DeleteUser();
        } 
        /// <summary>
        /// Controller Checks the data sent from client for null and passes it to PostUserDetails method of Users Service.
        /// </summary>
        /// <param name="users">Data of user from client</param>
        /// <returns>Returns the response from PostUserDetails method of Users Service</returns>
        [HttpPost("PostUser")]

        public async Task<bool> SignUp([FromBody] User users)
        {
            try
            {
                if (users == null)
                {
                    return false;
                }
                else
                {
                    return await _usersService.SubmitUserDetails(users);
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        /// <summary>
        /// Controller Checks the data sent from client for null and passes it to GetEmailValidation method of Users Service.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns>Returns the response from GetEmailValidation method of Users Service</returns>
        [HttpGet("ValidateEmail")]

        public async Task<bool> IsEmailAlreadyExist(string userEmail) 
        {
            try
            {
                if (userEmail == null)
                {
                    return false;
                }
                else
                {
                   var res = await _usersService.IsEmailAlreadyRegistered(userEmail);
                    return res;
                }
            }
            catch (Exception) 
            {
                return false;
            }
        }

       

    }
}
