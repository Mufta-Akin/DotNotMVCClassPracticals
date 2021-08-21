using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

using SMS.Data.Models;
using SMS.Data.Services;
using SMS.Web.Models;

namespace SMS.Web.Controllers
{
    public class StudentController : BaseController
    {
        private IStudentService svc;

        public StudentController()
        {
            svc = new StudentServiceDb();
        }

        // GET /student
        public IActionResult Index()
        {
            // complete this method
            var students = svc.GetStudents();
           
            return View(students);
        }

        // GET /student/details/{id}
        public IActionResult Details(int id)
        {
            // retrieve the student with specified id from the service
            var s = svc.GetStudent(id);

            // check if s is null and return NotFound()
            if (s == null)
            {     
               return NotFound();
            }

            // pass student as parameter to the view
            return View(s);
        }

        // GET: /student/create
        public IActionResult Create()
        {
            // display blank form to create a student
            var s = new Student();
            return View(s);
        }

        // POST /student/create
        [HttpPost]
        public IActionResult Create(Student s)
        {
            // check email is unique for this student

            // complete POST action to add student
            if (ModelState.IsValid)
            {
                // pass data to service to store 
                svc.AddStudent(s.Name, s.Course, s.Email, s.Age, s.Grade, s.PhotoUrl);

                return RedirectToAction(nameof(Index));
            }
           
            // redisplay the form for editing as there are validation errors
            return View(s);
        }

        // GET /student/edit/{id}
        public IActionResult Edit(int id) {
            var s = svc.GetStudent(id); // load the student using the service            
            if (s == null)  {           // check if s is null and alert
                return NotFound();
            }
            return View(s); // pass student to view for editing
        }

        // POST /student/edit/{id}
        [HttpPost]
        public IActionResult Edit(int id, Student s)
        {
            // check email is unique for this student
            

            // complete POST action to save student changes
            if (ModelState.IsValid)
            {
                // Pass data to service to update
                var updated = svc.UpdateStudent(s);
               
                return RedirectToAction(nameof(Index));     
            }
            // redisplay the form for editing as validation errors
            return View(s);
        }

        // GET / student/delete/{id}
        public IActionResult Delete(int id)
        {
            // load the student using the service
            var s = svc.GetStudent(id);
            // check the returned student is not null and if so alert
            if (s == null)
            {
               return NotFound();
            }     
            
            // pass student to view for deletion confirmation
            return View(s);
        }

        // POST /student/delete/{id}
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            // delete student via service
            svc.DeleteStudent(id);
         
            // redirect to the index view
            return RedirectToAction(nameof(Index));
        }

        // GET /student/createticket
        public IActionResult CreateTicket(int id)
        {
            var s = svc.GetStudent(id);
             // check the returned student is not null and if so alert
            if (s == null)
            {        
                return NotFound();
            }  
            // create the ticket view model and populate the StudentId property
            var t = new Ticket {
                StudentId = id
            };
 
            return View("CreateTicket", t);
        }

        // POST /student/createticket
        [HttpPost]
        public IActionResult CreateTicket(Ticket t)
        {
            // retrieve student using t.StudentId
           
            // check the returned student is not null and if so alert and 
            // redirect to Index view

            // create alert that ticket was created successfully            
            
            // create the ticket passing t.StudentId and t.Issue to relevant service method

            // redirect to Details view passing route parameter- new {Id = t.StudentId}
            return RedirectToAction("Index");
        }

    }
}