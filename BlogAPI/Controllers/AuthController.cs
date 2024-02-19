using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        public static Author author = new();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<Author> Register(AuthorDto request)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);


            author.Username = request.Username;
            author.PasswordHash = passwordHash;

            return Ok(author);
        }

        [HttpPost("Login")]
        public ActionResult<Author> Login(AuthorDto request)
        {
            if (author.Username != request.Username)
            {
                return BadRequest("author not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, author.PasswordHash))
            {
                return BadRequest("Wrong password");
            }

            var token = CreateToken(author);

            return Ok(token);
        }

        private string CreateToken(Author user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
