using CarPool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpGet("login")]
    public AuthenticatedResponse Login(string userEmail , string password)
    {
        if (userEmail == null || password == null)
        {
            return new AuthenticatedResponse { Token = null, Success = false };
        }
        else if (GlobalStorage.Users.Any(us=>us.UserEmail==userEmail) && GlobalStorage.Users.Any(us => us.Password == password))
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:7107",
                audience: "https://localhost:7107",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return new AuthenticatedResponse { Token = tokenString , Success=true};
        }
        else
        {
            return new AuthenticatedResponse { Token = null, Success = false };
        }
    }
}
