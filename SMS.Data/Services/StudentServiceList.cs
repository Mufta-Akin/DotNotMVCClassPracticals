using System;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;

using SMS.Data.Models;
using System.Net;

namespace SMS.Data.Services
{
    public class StudentServiceList : IStudentService
    {
        private readonly string STUDENT_STORE = "students.json";
       
        private IList<Student> Students;
      
        public StudentServiceList()
        {
            Load();
        }

        // load data from local json store
        private void Load()
        {
            try {
                string students = File.ReadAllText(STUDENT_STORE);  
                Students = JsonSerializer.Deserialize<List<Student>>(students);
            } 
            catch (Exception )
            {
                Students = new List<Student>();
            }
        }

        // write to local json store
        private void Store()
        {
            var students = JsonSerializer.Serialize(Students);
            File.WriteAllText(STUDENT_STORE, students);
          }

        public void Initialise()
        {
            Students.Clear(); // wipe all students
        }

        // ------------------ Student Related Operations ------------------------

        // retrieve list of Students
        public IList<Student> GetStudents()
        {
            return Students;
        }


        // Retrive student by Id 
        public Student GetStudent(int id)
        {
            return Students.FirstOrDefault(s => s.Id == id);
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
                // simple mechanism to calculate next Id
                Id = Students.Count + 1,
                Name = name,
                Course = course,
                Email = email,
                Age = age,
                Grade = grade,
                PhotoUrl = photo
            };
            Students.Add(s);
            Store(); // write to local file store
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
            Students.Remove(s);
            Store(); // write to local file store
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

            Store(); // write to local file store
            return student;
        }

        public Student GetStudentByEmail(string email)
        {
            return Students.FirstOrDefault(s => s.Email == email);
        }

        public IList<Student> GetStudentsQuery(Func<Student,bool> q)
        {
            return Students.Where(q).ToList();
        }

         // =================== Ticket Management ===================
        public Ticket CreateTicket(int studentId, string issue)
        {
            var student = GetStudent(studentId);
            if (student == null) return null;

            var ticket = new Ticket
            {
                Id = Students.Sum(s => s.Tickets.Count()) + 1,               
                Issue = issue,        
                StudentId = studentId,
                Student = student,
                // set by default in model but we can override here if required
                CreatedOn = DateTime.Now,
                Active = true                 
            };
            student.Tickets.Add(ticket);
            Store();
            return ticket;
        }
        public Ticket GetTicket(int id)
        {
            var ticket = Students.SelectMany(s => s.Tickets).FirstOrDefault(t => t.Id == id);
            return ticket;
        }
        public Ticket CloseTicket(int id)
        {
            var ticket = GetTicket(id);
            // if ticket does not exist or is already closed return null
            if (ticket == null  || !ticket.Active) return null;

            // ticket exists and is active so close
            ticket.Active = false;
            ticket.ResolvedOn = DateTime.Now;
            Store();
            return ticket;
        }

        public bool DeleteTicket(int id)
        {
            // find ticket
            var ticket = GetTicket(id);
            if (ticket == null) return false;
            
            // remove ticket from student
            var result = ticket.Student.Tickets.Remove(ticket);
            Store();
            return result;
        }

        public IList<Ticket> GetAllTickets()
        {
            var tickets = Students.SelectMany(
                s => s.Tickets.Select( t => 
                        new Ticket { Id = t.Id, 
                            CreatedOn = t.CreatedOn, 
                            ResolvedOn = t.ResolvedOn,
                            Active = t.Active, 
                            StudentId = s.Id,
                            Student = s
                        })
            ).ToList();

            return tickets;
        }
        public IList<Ticket> GetOpenTickets()
        {
            var tickets = Students.SelectMany(
                s => s.Tickets.Select ( t => 
                        new Ticket { Id = t.Id, 
                            CreatedOn = t.CreatedOn, 
                            ResolvedOn = t.ResolvedOn,
                            Active = t.Active, 
                            Issue = t.Issue,
                            Student = s
                        }).Where(t => t.Active)
                ).ToList();
            return tickets;
        }

        // Miscellaneous
        public bool IsDuplicateEmail(string email, int studentId) 
        {
            var existing = GetStudentByEmail(email);
            // if a student with email exists and the Id does not match
            // the studentId, then they cannot use the email
            return existing != null && studentId != existing.Id;           
        }

         
    }
}