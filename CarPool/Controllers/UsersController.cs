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

        public User GetUserDetails()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _usersService.GetUsers(userId);
        }

        /// <summary>
        /// Controller to return response from the GetUsers method of Users Service.
        /// </summary>
        /// <returns>Returns List of Users returned by GetUsers method of Users Service</returns>
        [HttpGet("GetUsers")]
        [Authorize]

        public User GetUsers(string userId)
        {
            return _usersService.GetUsers(userId);
        }


        /// <summary>
        /// Controller to return response from ChangePassword method of Users Service.
        /// </summary>
        /// <param name="newPass">New Password value</param>
        /// <returns>returns response as bool</returns>
        [HttpPatch("ChangePassword")]
        [Authorize]
        public bool ChangePassword(string newPass)
        {
            if(newPass!=null)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return _usersService.ChangePassword(userId, newPass).Result;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Controller to return response from UpdateProfile method of Users Service.
        /// </summary>
        /// <param name="userName">new userName Value</param>
        /// <param name="profileImage">new Profile Image value</param>
        /// <returns>returns response as bool</returns>
        [HttpPatch("UpdateProfile")]
        [Authorize]
        public bool UpdateProfile([FromBody]User user)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _usersService.UpdateProfile(userId, user.UserName, user.ProfileImage).Result;
        }

        /// <summary>
        /// Controller to return response from Delete Profile method of Users Service.
        /// </summary>
        /// <returns>returns response as bool</returns>
        [HttpDelete("DeleteProfile")]
        [Authorize]
        public bool DeleteProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _usersService.DeleteProfile(userId).Result;
        } 
        /// <summary>
        /// Controller Checks the data sent from client for null and passes it to PostUserDetails method of Users Service.
        /// </summary>
        /// <param name="users">Data of user from client</param>
        /// <returns>Returns the response from PostUserDetails method of Users Service</returns>
        [HttpPost("PostUser")]

        public bool PostUser([FromBody] User users)
        {
            try
            {
                if (users == null)
                {
                    return false;
                }
                else
                {
                    return _usersService.PostUserDetails(users).Result;
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

        public bool GetEmailValidation(string userEmail) 
        {
            try
            {
                if (userEmail == null)
                {
                    return false;
                }
                else
                {
                    return _usersService.ValidateEmail(userEmail);
                }
            }
            catch (Exception) 
            {
                return false;
            }
        }
    }
}
