using System;
using Microsoft.AspNetCore.Mvc;
using SMS.Web.Models;
using SMS.Data.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SMS.Web.Controllers
{
    public class TicketController : BaseController
    {
        private readonly IStudentService svc;
        
        public TicketController()
        {
            svc = new StudentServiceDb();
        }

        // GET /ticket/index
        //public IActionResult Index()
        //{
            // get all open tickets

            // pass tickets to view
       // }
       
        //  POST /ticket/close/{id}
        //[HttpPost]
        //public IActionResult Close(int id)
        //{
            // close ticket via service
           
            

            // redirect to the index view
            
        //}
       
        // GET /ticket/create
        //public IActionResult Create()
        //{
            // retrieve all students
            // var students = ...
            
            // create a TicketViewModel and set the Students property
            // to new SelectList(students,"Id","Name")
            
            
            // render blank form
            
        //}
       
        // POST /ticket/create
        //[HttpPost]
        //public IActionResult Create(TicketViewModel tvm)
        //{
            // if ticketviewmodel is valid
                // create ticket
                // redirect to Index
            // endif
            
            // redisplay the form for editing as validation failed 
        //}
    }
}
