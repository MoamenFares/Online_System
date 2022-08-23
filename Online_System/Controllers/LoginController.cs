using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_System.Data;
using Online_System.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace Online_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        Online_Store_Context Db;
        public LoginController(Online_Store_Context db)
        {
            this.Db = db;
        }
        [HttpPost]
        public async Task<IActionResult> Login(usr usr)
        {
            if (usr.UserName != null && usr.Password != null)
            {
                User u = Db.Users.Where(a => a.UserName == usr.UserName && a.Password == usr.Password).FirstOrDefault();
                if (u != null)
                {
                    //create Token

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("LinkDev_Online_Store_key"));

                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var data = new List<Claim>();
                    data.Add(new Claim("Id", u.Id.ToString()));
                    if (u.IsAdmin)
                    {
                        data.Add(new Claim("Role", "Admin"));
                    }
                    else
                        data.Add(new Claim("Role", "Customer"));


                    var token = new JwtSecurityToken(
                    claims: data,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        //[HttpPost("/api/Logout")]
        //public async Task<IActionResult> Logout(int Id)
        //{
        //    if (Id != 0)
        //    {
        //        await Logout(Id);
        //        return NoContent();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
    }
    public class usr
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
