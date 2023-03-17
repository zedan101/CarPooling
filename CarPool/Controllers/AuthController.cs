using Carpool.DataLayer;
using CarPool.Model;
using CarPool.Models;
using CarPool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarPool.Controllers
{
    /// <summary>
    /// Controller for Authenticating the user login credentials and generating jwt token.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly CarPoolContext _carPoolContext;
       
        /// <summary>
        /// member of AuthController class
        /// </summary>
        private readonly IUsersService _usersService;

        /// <summary>
        /// Constructor of AuthController Class
        /// </summary>
        /// <param name="usersService">Instence of IUSerService interface</param>
        public AuthController(IUsersService usersService, CarPoolContext carPoolContext)
        {
            _usersService = usersService;
            _carPoolContext = carPoolContext;
        }

        /// <summary>
        /// It passes the userEmail and password to ValidUser method of UsersService and if credentials are valid  it generates a new token else assign null to token.
        /// </summary>
        /// <param name="userEmail">Email entered by user at login page </param>
        /// <param name="password">Password entered by user at the login page</param>
        /// <returns>Returns jwt token if user is valid</returns>
        [HttpGet("login")]
        public AuthenticatedResponse Login(string userEmail, string password)
        {
            if (_usersService.ValidateUser(userEmail, password))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, _carPoolContext.User.First(user=>user.UserEmail==userEmail).UserId)
                };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7107",
                    audience: "https://localhost:7107",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return new AuthenticatedResponse { Token = tokenString };
            }
            else
            {
                return new AuthenticatedResponse { Token = null };
            }
        }
    }
}