using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SBIMFJWT.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SBIMFJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            if(login.Username == "admin" &&  login.Password == "123")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, login.Username),
                    new Claim(ClaimTypes.Role,"Admin")
                };
                var keys=new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("AbhishekSecurityKey"));
                var creds=new SigningCredentials(keys,SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    audience: "Abhishek",
                    issuer: "User",
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
                    signingCredentials: creds
                    );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return Unauthorized("Invalid Username And Password");
        }
    }
}
