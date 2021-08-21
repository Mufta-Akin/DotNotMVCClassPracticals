using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

using SMS.Data.Models;
using SMS.Data.Services;
using SMS.Web.Models;

namespace SMS.Web.Controllers
{
    [Authorize]
    public class TicketController : BaseController
    {
        private readonly IStudentService svc;
  
        // configured via DI
        public TicketController(IStudentService ss)
        {
            svc = ss;
        }

        public IActionResult Search()
        {
            return View("LitSearch");
        }
        
        // GET /ticket/index
        public IActionResult Index()
        {
            // retrieve all OPEN tickets   
            var search  =  new TicketSearchViewModel {
                Tickets = svc.SearchTickets(TicketRange.OPEN, "")
            };
            return View(search);
        }  

        // POST /ticket/index   
        [HttpPost]    
        public IActionResult Index(TicketSearchViewModel search)
        {            
            // perform search query and assign results to viewmodel Tickets property
            search.Tickets = svc.SearchTickets(search.Range, search.Query);

            // build custom alert message if post           
            var alert = $"{search.Tickets.Count} result(s) found searching '{search.Range}' Tickets";
            if (search.Query != null && search.Query != "")
            {
                alert += $" for '{search.Query}'"; 
            }

            // display custom info alert
            Alert(alert, AlertType.info); 

            return View("Index", search);
        }     
             
        // GET/ticket/{id}
        public IActionResult Details(int id)
        {
            var ticket = svc.GetTicket(id);
            if (ticket == null)
            {
                Alert("Ticket Not Found", AlertType.warning);  
                return RedirectToAction(nameof(Index));             
            }

            return View(ticket);
        }

        // POST /ticket/close/{id}
        [HttpPost]
        [Authorize(Roles="admin,manager")]
        public IActionResult Close([Bind("Id, Resolution")] Ticket t)
        {
            // close ticket via service
            var ticket = svc.CloseTicket(t.Id, t.Resolution);
            if (ticket == null)
            {
                Alert("Ticket Not Found", AlertType.warning);                               
            }
            else
            {
                Alert($"Ticket {t.Id } closed", AlertType.info);  
            }

            // redirect to the index view
            return RedirectToAction(nameof(Index));
        }
       
        // GET /ticket/create
        [Authorize(Roles="admin,manager")]
        public IActionResult Create()
        {
            var students = svc.GetStudents();
            // populate viewmodel select list property
            var tvm = new TicketCreateViewModel {
                Students = new SelectList(students,"Id","Name") 
            };
            
            // render blank form
            return View( tvm );
        }
       
        // POST /ticket/create
        [HttpPost]
        [Authorize(Roles="admin,manager")]
        public IActionResult Create(TicketCreateViewModel tvm)
        {
            if (ModelState.IsValid)
            {
                svc.CreateTicket(tvm.StudentId, tvm.Issue);
     
                Alert($"Ticket Created", AlertType.info);  
                return RedirectToAction(nameof(Index));
            }
            
            // redisplay the form for editing
            return View(tvm);
        }

    }
}
