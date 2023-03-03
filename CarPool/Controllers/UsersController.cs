using Microsoft.AspNetCore.Mvc;
using CarPool.Models;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using CarPool.Services.Interfaces;

namespace CarPool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("GetUserDetails")]
        [Authorize]

        public Users GetUserDetails(string userId)
        {
            return _usersService.GetUsers(userId);
        }

        [HttpGet("GetUsers")]
        [Authorize]

        public List<Users> GetUsers()
        {
            return _usersService.GetUsers();
        }

        [HttpPost("PostUser")]
        [Authorize]

        public bool PostUser([FromBody] Users users)
        {
            try
            {
                if (users == null)
                {
                    return false;
                }
                else
                {
                    return _usersService.PostUserDetails(users);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        

        [HttpGet("ValidateEmail")]
        [Authorize]

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
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
