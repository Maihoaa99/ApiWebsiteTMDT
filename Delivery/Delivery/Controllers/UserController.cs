using Delivery.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Delivery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _accountContext;
        public UserController(DatabaseContext accountContext) {
            _accountContext = accountContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Accounts>> GetUser()
        {
            return _accountContext.Accounts;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Accounts>> GetUser(Guid id)
        {
            var user = await _accountContext.Accounts.FindAsync(id);
            return user;
        }

        [HttpPost]
        public ActionResult<Accounts> AddUser([FromBody] Accounts accounts)
        {
            // Băm mật khẩu trước khi lưu vào cơ sở dữ liệu
            string hashedPassword = HashPassword(accounts.password);
            // Gán mật khẩu đã băm vào đối tượng Accounts
            accounts.password = hashedPassword;
            _accountContext.Accounts.Add(accounts);
            _accountContext.SaveChanges();
            return CreatedAtAction(nameof(AddUser), new { id = accounts.id }, accounts);
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

        [HttpPut]
        public async Task<IActionResult> PutUser(Guid id, Accounts accounts)
        {
            _accountContext.Entry(accounts).State = EntityState.Modified;
            try
            {
                await _accountContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool UserAvailable(Guid id)
        {
            return (_accountContext.Accounts?.Any(x => x.id == id)).GetValueOrDefault();    
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var account = await _accountContext.Accounts.FindAsync(id);
            _accountContext.Accounts.Remove(account);
            await _accountContext.SaveChangesAsync();
            return Ok();
        }
    }
}
