using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

using SMS.Data.Services;
using SMS.Data.Models;

using SMS.Rest.Models;

namespace SMS.Rest.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IStudentService svc;       
        private readonly string secret;     
           
        public UserController(IStudentService service, IConfiguration config) 
        {
            svc = service;
            secret = config.GetValue<string>("JwtConfig:Secret");
        }

        // POST api/user/login
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<User> Login(UserLoginModel login)        
        {                     
            var user = svc.Authenticate(login.Email, login.Password);            
            if (user == null)
            {
                return BadRequest(new { message = "Email or Password is incorrect" });
            }
            return AuthBuilder.SignJwtToken(user, secret);
        }

    }
}
