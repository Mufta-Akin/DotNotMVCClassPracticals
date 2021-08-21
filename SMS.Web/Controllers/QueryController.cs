using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using SMS.Data.Models;
using SMS.Data.Services;
using SMS.Web.Models;

namespace SMS.Web.Controllers
{

    [ApiController]    
    [Route("api")] //[Route("api/[controller]")]    
    // set default auth scheme as we are using both cookie and jwt authentication
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QueryController : ControllerBase
    {
        private IStudentService svc;       
        private readonly string secret; // jwt secret
      
        public QueryController(IStudentService service, IConfiguration config)
        {      
            // retrieve secret from appsettings to use when signing jwt token in login action
            secret = config.GetValue<string>("JwtConfig:Secret");            
            svc = service;
        }

        // POST api/user/login
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<User> Login(UserLoginViewModel login)        
        {                     
            var user = svc.Authenticate(login.Email, login.Password);            
            if (user == null)
            {
                return BadRequest(new { message = "Email!!!!! or Password is incorrect" });
            }
            // sign jwt token to use in secure api requests
            var userWithToken = AuthBuilder.SignJwtToken(user, secret);
            return Ok(userWithToken);
        }       

        [HttpGet("tickets")]
        public ActionResult<IList<Ticket>> GetTickets()
        {
            var tickets = svc.GetAllTickets(); //.SearchTickets(TicketRange.ALL,""); 
            return Ok(tickets);
        }

        [HttpGet("tickets/id/{id}")]
        public ActionResult<Ticket> GetTicket(int id)
        {
            var ticket = svc.GetTicket(id);
            if (ticket == null) 
            {
                return NotFound();
            }

            var vm = new TicketViewModel {
                    Id = ticket.Id,
                    Issue = ticket.Issue,
                    Resolution = ticket.Resolution,
                    CreatedOn = ticket.CreatedOn,
                    ResolvedOn = ticket.ResolvedOn,
                    Active = ticket.Active,
                    StudentId = ticket.StudentId,
                    StudentName = ticket.Student.Name
            };
            return Ok(vm);
        }


        [HttpPost("tickets")]
        public ActionResult<DemoViewModel> Create(TicketCreateViewModel tvm)
        {
            if (ModelState.IsValid)
            {
                var ticket = svc.CreateTicket(tvm.StudentId, tvm.Issue);
       
                return CreatedAtAction(nameof(GetTicket), new { Id = ticket.Id }, ticket );                    
            }
                    
            return BadRequest("Ticket could not be created");
        }

        [HttpGet("tickets/search/{range}/{query?}")] 
        public ActionResult<IList<TicketViewModel>> QueryTicket(TicketRange range = TicketRange.OPEN, string query = "")
        {           
            var tickets = svc.SearchTickets(range, query)
                .Select( t => new TicketViewModel {
                    Id = t.Id,
                    Issue = t.Issue,
                    Resolution = t.Resolution,
                    CreatedOn = t.CreatedOn,
                    ResolvedOn = t.ResolvedOn,
                    Active = t.Active,
                    StudentId = t.StudentId,
                    StudentName = t.Student.Name
                })
                .ToList();
            return Ok(tickets);            
        }     

    }
}