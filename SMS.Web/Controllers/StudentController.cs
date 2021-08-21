using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SMS.Data.Models;
using SMS.Data.Services;
using SMS.Web.Models;

namespace SMS.Web.Controllers
{
    [Authorize]
    public class StudentController : BaseController
    {
        private IStudentService svc;

        // Configured via DI
        public StudentController(IStudentService ss)
        {
            svc = ss;
        }

        // GET /student/index
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
                Alert("Student Not Found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // pass student as parameter to the view
            return View(s);
        }

        // GET: /student/create
        [Authorize(Roles="admin")]
        public IActionResult Create()
        {
            // display blank form to create a student
            var s = new Student();
            return View(s);
        }

        // POST /student/create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="admin")]
        public IActionResult Create([Bind("Name, Email, Course, Age, Grade, PhotoUrl")] Student s) 
        {
            // check email is unique for this student
            if (svc.IsDuplicateEmail(s.Email, s.Id))
            {
                // add manual validation error
                ModelState.AddModelError(nameof(s.Email),"The email address is already in use");
            }

            // validate student
            if (ModelState.IsValid)
            {
                // pass data to service to store 
                var added = svc.AddStudent(s.Name, s.Course, s.Email, s.Age, s.Grade, s.PhotoUrl);
                Alert("Student created successfully", AlertType.info);
                
                return RedirectToAction(nameof(Index));
            }
           
            // redisplay the form for editing as there are validation errors
            return View(s);
        }

        // GET /student/edit/{id}
        [Authorize(Roles="admin,manager")]
        public IActionResult Edit(int id)
        {
            // load the student using the service
            var s = svc.GetStudent(id);

            // check if s is null and return NotFound()
            if (s == null)
            {
                Alert($"No such student {id}", AlertType.warning); 
                return RedirectToAction(nameof(Index));
            }   

            // pass student to view for editing
            return View(s);
        }

        // POST /student/edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="admin,manager")]
        public IActionResult Edit(int id, [Bind("Id, Name, Email, Course, Age, Grade, PhotoUrl")] Student s)
        {
            // check email is unique for this student
            if (svc.IsDuplicateEmail(s.Email, s.Id))
            {
                // add manual validation error
                ModelState.AddModelError(nameof(s.Email),"The email address is already in use");
            } 
            
            // validate student
            if (ModelState.IsValid)
            {
                // pass data to service to update
                svc.UpdateStudent(s);
                Alert("Student details saved", AlertType.info);

                return RedirectToAction(nameof(Index));
            }

            // redisplay the form for editing as validation errors
            return View(s);
        }

        // GET / student/delete/{id}
        [Authorize(Roles="admin")]       
        public IActionResult Delete(int id)
        {
            // load the student using the service
            var s = svc.GetStudent(id);
            // check the returned student is not null and if so return NotFound()
            if (s == null)
            {
                Alert("Student Not Found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }     
            
            // pass student to view for deletion confirmation
            return View(s);
        }

        // POST /student/delete/{id}
        [HttpPost]
        [Authorize(Roles="admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            // delete student via service
            svc.DeleteStudent(id);
         
            Alert($"Student {id} deleted successfully", AlertType.success);
            // redirect to the index view
            return RedirectToAction(nameof(Index));
        }


        // GET /student/createticket
        [Authorize(Roles="admin,manager")]
        public IActionResult CreateTicket(int id)
        {
            var s = svc.GetStudent(id);
            // check the returned student is not null and if so alert
            if (s == null)
            {
                Alert($"No such student {id}", AlertType.warning);          
                return RedirectToAction(nameof(Index));
            }   
            // create the ticket view model and populate the StudentId property
            var t = new TicketCreateViewModel {
                StudentId = id
            };
            
            return View("CreateTicket", t);
        }
        

        // POST /student/createticket
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="admin,manager")]
        public IActionResult CreateTicket([Bind("StudentId, Issue")]TicketCreateViewModel m)
        {
            var s = svc.GetStudent(m.StudentId);
             // check the returned student is not null and if so return NotFound()
            if (s == null)
            {
                Alert($"No such student {m.StudentId}", AlertType.warning);          
                return RedirectToAction(nameof(Index));
            }  
        
            // create the ticket view model and populate the StudentId property
            svc.CreateTicket(m.StudentId, m.Issue);
            Alert($"Ticket created successfully", AlertType.success);   

            return RedirectToAction("Details", new { Id = m.StudentId });
        }


        // GET /student/search/{query}
        public IActionResult Search(string query)
        {
            var results = svc.GetStudentsQuery(s => s.Name != null && s.Name.ToLower().Contains(query.ToLower()));
            return View("Index", results);
        }

    }
}
