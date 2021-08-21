
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using SMS.Data.Models;
using SMS.Data.Repositories;
using SMS.Data.Security;

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
            return db.Students
                     .Include(s => s.Tickets)
                     .Where(q).ToList();
        }

         // Miscellaneous
        public bool IsDuplicateEmail(string email, int studentId) 
        {
            var existing = GetStudentByEmail(email);
            // if a student with email exists and the Id does not match
            // the studentId (if provided), then they cannot use the email
            return existing != null && studentId != existing.Id;           
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

        // return ticket and related student
        public Ticket GetTicket(int id)
        {
            return db.Tickets
                     .Include(t => t.Student)
                     .FirstOrDefault(t => t.Id == id);
        }

        // Closed the specified ticket - must exist and not already closed
        public Ticket CloseTicket(int id, string resolution)
        {
            var ticket = GetTicket(id);
            // if ticket does not exist or is already closed return null
            if (ticket == null || ticket.Active == false) return null;
            
            // ticket exists and is active so close
            ticket.Active = false;
            ticket.Resolution = resolution;
            ticket.ResolvedOn = DateTime.Now;
            
            db.SaveChanges(); // write to database
            return ticket;  // return closed ticket
        }

        // delete specified ticket returning true if successful otherwise false
        public bool DeleteTicket(int id)
        {
            // find ticket
            var ticket = GetTicket(id);
            if (ticket == null) return false;
            
            // remove ticket from student
            var result = ticket.Student.Tickets.Remove(ticket);
            db.SaveChanges();

            return result;  
        }

        // return all tickets and the student generating the ticket
        public IList<Ticket> GetAllTickets()
        {
            var tickets = db.Tickets
                            .Include(t => t.Student)
                            .ToList();
            return tickets;
        }

        // get only active tickets and the student generating the ticket
        public IList<Ticket> GetOpenTickets()
        {
             return db.Tickets
                      .Include(t => t.Student)
                      .Where(t => t.Active)
                      .ToList();
        } 
        
        public IList<Ticket> GetTicketsQuery(Func<Ticket,bool> q)
        {
            return db.Tickets
                     .Include(s => s.Student)
                     .Where(q).ToList();
        }
        
        // perform a search of the tickets based on a query and
        // an active range 'ALL', 'OPEN', 'CLOSED'

        public IList<Ticket> SearchTickets(TicketRange range, string query) 
        {
            // ensure query is not null    
            query ??= "";
        
            // search student name
            var r1 = db.Tickets 
                       .Include(t => t.Student)
                       .Where(t => t.Student.Name.ToLower().Contains(query.ToLower()));
            // search ticket issue
            var r2 = db.Tickets
                       .Include(t => t.Student)
                       .Where(t => t.Issue.ToLower().Contains(query.ToLower()));

            // Use Union to join both queries and Where to filter by ticket status 
            // Calling ToList() actually executes the final combined query
            return  r1.Union(r2).Where(t => 
                    range == TicketRange.OPEN && t.Active ||
                    range == TicketRange.CLOSED && !t.Active ||
                    range == TicketRange.ALL
            ).ToList();  
        }


        public IList<Ticket> SearchTicketsLongWinded(string range, string query) 
        {
            // convert query to lowercase
            query = query == null ? "" : query.ToLower();
            range = range == null ? "ALL" : range.ToUpper();
           
            // search based on query and range
            if (range == "ALL")
            {
                var r1 = db.Tickets 
                       .Include(t => t.Student)
                       .Where(t => t.Student.Name.ToLower().Contains(query));
                var r2 = db.Tickets
                       .Include(t => t.Student)
                       .Where(t => t.Issue.ToLower().Contains(query));
                
                // combine both queries (ensuring no duplicates) and execute
                return r1.Union(r2).ToList();
            }
            else if (range == "CLOSED")
            {
                var r1 =  db.Tickets 
                            .Include(t => t.Student)
                            .Where(t =>  !t.Active && t.Student.Name.ToLower().Contains(query)); 
                var r2 =  db.Tickets 
                            .Include(t => t.Student)
                            .Where(t => !t.Active && t.Issue.ToLower().Contains(query));

                // combine both queries (ensuring no duplicates) and execute
                return r1.Union(r2).ToList();
            }
            else
            {
                var r1 =  db.Tickets 
                            .Include(t => t.Student)
                            .Where(t => t.Active && t.Student.Name.ToLower().Contains(query)); 
                var r2 =  db.Tickets 
                            .Include(t => t.Student)
                            .Where(t => t.Active && t.Issue.ToLower().Contains(query));
                
                // combine both queries (ensuring no duplicates) and execute
                return r1.Union(r2).ToList();
            }             
        }

         // ------------------ User Related Operations ------------------------

        // Retrive user by email 
        public User GetUserByEmail(string email)
        {
            return db.Users.FirstOrDefault(u => u.Email == email);
        }
     
        // Authenticate a user
        public User Authenticate(string email, string password)
        {
            // retrieve the user based on the Email address (assumes Email is unique)
            var user = GetUserByEmail(email);

            // Verify the user exists and Hashed User password matches the password provided
            // return user if authenticated otherwise null
            return (user != null && Hasher.ValidateHash(user.Password, password)) ? user : null;            
        }

      
        // Register a new user
        public User Register(string name, string email, string password, Role role)
        {
            // check that the user does not already exist (unique user name)
            var exists = GetUserByEmail(email);
            if (exists != null)
            {
                return null;
            }

            // create user
            var user = new User 
            {
                Name = name,
                Email = email,
                Password = Hasher.CalculateHash(password),
                Role = role   
            };
                
            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }     

    }
   
}