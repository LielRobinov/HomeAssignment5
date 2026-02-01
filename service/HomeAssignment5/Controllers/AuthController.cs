using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeAssignment5.Controllers
{
    // מחלקה לקבלת נתוני התחברות
    public class LoginRequest
    {
        //? = מאפשר למחרוזת שתהיה null
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string userName = "admin";
        private readonly string password = "1234";
        private readonly string SECRET_KEY = "THIS_IS_A_SUPER_SECRET_KEY_12345"; // כל מחרוזת ארוכה

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // בדיקה בסיסית
            if (request.Username != userName || request.Password != password)
                return Unauthorized(); // מחזיר 401 אם שגוי

            // יצירת Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username)
            };

            // מפתח לחתימה
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // יצירת טוקן
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            // החזרת הטוקן למשתמש
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}

