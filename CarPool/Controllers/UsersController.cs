using Microsoft.AspNetCore.Mvc;
using CarPool.Models;
using Microsoft.AspNetCore.Cors;
using CarPool.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        [Route("GetUserDetails")]
        [Authorize]

        public Users GetUserDetails(string userId)
        {
            return _usersService.GetUsers(userId);
        }

        [HttpGet]
        [Route("GetUsers")]
        [Authorize]

        public List<Users> GetUsers()
        {
            return _usersService.GetUsers();
        }

        [HttpPost]
        [Route("PostUser")]
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

        /*[HttpGet]
        [Route("ValidateUser")]
        [Authorize]

        public bool ValidateUser(string userEmail, string password)
        {
            try
            {
                if (userEmail == null || password == null)
                {
                    return false;
                }
                else
                {
                    return _usersService.ValidateUser(userEmail, password);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }*/

        [HttpGet]
        [Route("ValidateEmail")]
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
