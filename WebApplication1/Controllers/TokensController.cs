using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        public IConfiguration _configuration;
private readonly FilmDBContext _context;



public TokensController(IConfiguration config, FilmDBContext context)
{
_configuration = config;
_context = context;
}
     
        
        
        [HttpPost]
        public async Task<IActionResult> Post(UserInformations _userData)
        {



            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Email, _userData.Password);



                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
new Claim("Id", user.Id.ToString()),
new Claim("FirstName", user.Firstname),
new Claim("LastName", user.Lastname),
new Claim("Email", user.Email),
};



                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));



                    var logIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: logIn);



                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<UserInformations> GetUser(string email, string password)
        {
            UserInformations userInfo = null;
            var result = await _context.userinfo.Where(u => u.Email == email && u.Password == password).ToListAsync();
            if (result.Count > 0)
                userInfo = result[0];
            return userInfo;



        }

    }
}

