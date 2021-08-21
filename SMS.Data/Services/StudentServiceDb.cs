using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using SMS.Data.Models;
using SMS.Data.Repositories;

namespace SMS.Data.Services
{
    public class StudentServiceDb : IStudentService
    {
        private readonly DatabaseContext db;

        public StudentServiceDb()
        {
            db = new DatabaseContext();
        }

        public void Initialise()
        {
            db.Initialise();
        }

        // ------------------ Student Related Operations ------------------------

        // retrieve list of Students
        public IList<Student> GetStudents()
        {
            // return the collection as a list
            return db.Students.ToList();
        }


        // Retrive student by Id 
        public Student GetStudent(int id)
        {
            return db.Students
                     .Include(s => s.Tickets)
                     .FirstOrDefault(s => s.Id == id);
        }

        // Add a new student checking a student with same email does not exist
        public Student AddStudent(string name, string course, string email, int age, double grade, string photo)
        {
            // check if email is already in use by another student
            var existing = GetStudentByEmail(email);
            if (existing != null)
            {
                return null; // email in use so we cannot create student
            } 
            // email is unique so go ahead an create student
            var s = new Student
            {
                //  Id is set automatically by the database
                Name = name,
                Course = course,
                Email = email,
                Age = age,
                Grade = grade,
                PhotoUrl = photo
            };
            db.Students.Add(s);
            db.SaveChanges(); // write to database
            return s; // return newly added student
        }

        // Delete the student identified by Id returning true if deleted and false if not found
        public bool DeleteStudent(int id)
        {
            var s = GetStudent(id);
            if (s == null)
            {
                return false;
            }
            db.Students.Remove(s);
            db.SaveChanges(); // write to database
            return true;
        }

        // Update the student with the details in updated 
        public Student UpdateStudent(Student updated)
        {
            // verify the student exists
            var student = GetStudent(updated.Id);
            if (student == null)
            {
                return null;
            }
            // update the details of the student retrieved and save
            student.Name = updated.Name;
            student.Email = updated.Email;
            student.Course = updated.Course;
            student.Age = updated.Age;
            student.Grade = updated.Grade;

            db.SaveChanges(); // write to database
            return student;
        }

        public Student GetStudentByEmail(string email)
        {
            return db.Students.FirstOrDefault(s => s.Email == email);
        }

        public IList<Student> GetStudentsQuery(Func<Student,bool> q)
        {
            return db.Students.Where(q).ToList();
        }

        // =================== Ticket Management ===================
        public Ticket CreateTicket(int studentId, string issue)
        {
            var student = GetStudent(studentId);
            if (student == null) return null;

            var ticket = new Ticket
            {
                // Id created by Database
                Issue = issue,        
                StudentId = studentId,
                // set by default in model but we can override here if required
                CreatedOn = DateTime.Now,
                Active = true,
            };
            student.Tickets.Add(ticket);
            db.SaveChanges(); // write to database
            return ticket;
        }
        public Ticket GetTicket(int id)
        {
            var ticket = db.Students.SelectMany(s => s.Tickets).FirstOrDefault(t => t.Id == id);
            return ticket;
        }
        public Ticket CloseTicket(int id)
        {
            var ticket = GetTicket(id);
            // if ticket does not exist or is already closed return null
            if (ticket == null || !ticket.Active) return null;
            
            // ticket exists and is active so close
            ticket.Active = false;
            ticket.ResolvedOn = DateTime.Now;
            db.SaveChanges(); // write to database
            return ticket;
        }

        public bool DeleteTicket(int id)
        {
            // find ticket
            var ticket = GetTicket(id);
            if (ticket == null) return false;
            
            // remove ticket 
            var result = ticket.Student.Tickets.Remove(ticket);
            
            db.SaveChanges();
            return result;
        }

        // Retrieve all tickets and the student associated with the ticket
        public IList<Ticket> GetAllTickets()
        {
            return db.Tickets
                     .Include(t => t.Student)
                     .ToList();
        }

        // Retrieve all open tickets and the student associated with the ticket
        public IList<Ticket> GetOpenTickets()
        {
            return db.Tickets
                     .Include(t => t.Student)
                     .Where(t => t.Active)
                     .ToList();
            
        } 

        // Miscellaneous
        public bool IsDuplicateEmail(string email, int studentId) 
        {
            var existing = GetStudentByEmail(email);
            // if a student with email exists and the Id does not match
            // the studentId (if provided), then they cannot use the email
            return existing != null && studentId != existing.Id;           
        }

    }
   
}