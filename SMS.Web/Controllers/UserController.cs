using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using SMS.Data.Services;
using SMS.Data.Models;
using SMS.Web.Models;

namespace SMS.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IStudentService _svc;

        public UserController()
        {
            _svc = new StudentServiceDb();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")]UserViewModel m)
        {        
            // call service to Authenticate User
            var user = _svc.Authenticate(m.Email, m.Password);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid Login Credentials");
                ModelState.AddModelError("Password", "Invalid Login Credentials");
                return View(m);
            }
           
            // sign user in using cookie authentication to store principal
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                BuildClaimsPrincipal(user)
            );
            return RedirectToAction("Index","Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        // add validate anti forgery token
        public IActionResult Register(/* add bind attribute */ UserViewModel m)
        {
            // Q2

            // if not valid model
            //    return View(m) to redisplay the view with validation errors
            // endif                               

            // call service to register user
            
            // if user returned is null then 
            //    add a modelstate error as email address must already be registered
            //   (see how this is done in login above) and return the view for editing           
            // endif 
 
            // Add alert indicating registration successful and redirect to login page

            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ErrorNotAuthorised()
        {   
            Alert("Not Authorized", AlertType.warning);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ErrorNotAuthenticated()
        {
            Alert("Not Authenticated", AlertType.warning);
            return RedirectToAction("Login", "User"); 
        }        

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyEmailAddress(string email)
        {
            if (_svc.GetUserByEmail(email) != null)
            {
                return Json($"Email Address {email} is already in use. Please choose another");
            }
            return Json(true);
        }

        // ========================= Build Claims Principle ==================
        // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-5.0
        // https://andrewlock.net/introduction-to-authentication-with-asp-net-core/
        
        // return claims principal based on authenticated user
        private  ClaimsPrincipal BuildClaimsPrincipal(User user)
        { 
            // define user claims
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())                              
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            // build principal using claims
            return  new ClaimsPrincipal(claims);
        }

    }
}
