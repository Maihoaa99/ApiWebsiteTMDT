using Delivery.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Delivery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    { 
        public IConfiguration _configuration;
        private readonly AccountLoginContext _context;
        
        public AccountController(IConfiguration config ,AccountLoginContext context)
        {
            _configuration = config; 
            _context = context;
        }

        private async Task<AccountLogin> GetAccount(string username, string password)
        {
            var client = new HttpClient();
            var field = typeof(System.Net.Http.Headers.HttpRequestHeaders)
                            .GetField("invalidHeaders", System.Reflection.BindingFlags.NonPublic 
                            | System.Reflection.BindingFlags.Static) ?? typeof(System.Net.Http.Headers.HttpRequestHeaders)
                            .GetField("s_invalidHeaders", System.Reflection.BindingFlags.NonPublic 
                            | System.Reflection.BindingFlags.Static);
            if (field != null)
            {
                var invalidFields = (HashSet<string>)field.GetValue(null);
                invalidFields.Remove("Content-Type");
            }
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            return await _context.accountLogins.FirstOrDefaultAsync(u => u.username == username && u.password == password);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AccountLogin accountLogin)
        {
            // Băm mật khẩu trước khi lưu vào cơ sở dữ liệu
            string hashedPassword = HashPassword(accountLogin.password);
            // Gán mật khẩu đã băm vào đối tượng Accounts
            accountLogin.password = hashedPassword;
            if (accountLogin != null && accountLogin.username != null && accountLogin.password != null)
            {
                var account = await GetAccount(accountLogin.username, accountLogin.password);
                if(account != null)
                {
                    //create claims details based on the account information
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        
                        new Claim("id", account.id.ToString())
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1),
                                signingCredentials: signIn);
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return Unauthorized("Invalid creadentials");
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hashedPassword = ByteArrayToHexString(hashedBytes);
                return hashedPassword;
            }
        }

        private string ByteArrayToHexString(byte[] array)
        {
            StringBuilder hex = new StringBuilder(array.Length * 2);
            foreach (byte b in array)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
    }
}
