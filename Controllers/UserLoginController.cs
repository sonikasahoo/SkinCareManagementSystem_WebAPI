using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Case_Study_SkinCareManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;



namespace Case_Study_SkinCareManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private IConfiguration cfg;
        public UserLoginController(IConfiguration configuration)
        {
            cfg = configuration;
        }
        private User AuthenticateUser(User user)
        {
            User user1 = null;
            if (user.UserName == "sonika" && user.Password == "happy")
            {
                user1 = new User { UserName = "Sonika Sahoo" };
            }
            return user1;
        }



        private string GenerateToken(User user)
        {
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["Jwt:Key"]));
            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);





            var token = new JwtSecurityToken(cfg["Jwt:Issuer"], cfg["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(20), signingCredentials: Credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);



        }



        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(User user)
        {
            IActionResult response = Unauthorized();
            var user1 = AuthenticateUser(user);
            if (user1 != null)
            {
                var token = GenerateToken(user1);
                response = Ok(new
                {
                    token
                 = token
                });
            }
            return response;



        }
    }
}