// using System;
// using System.IO;
// using System.Text.Json;
// using System.Linq;
// using System.Collections.Generic;

// using SMS.Data.Models;

// namespace SMS.Data.Services
// {
//     public class StudentServiceList : IStudentService
//     {
//         private readonly string STUDENT_STORE = "students.json";
//         private readonly string USER_STORE = "users.json";
    
//         private IList<Student> Students;
//         private IList<User> Users;
        
//         public StudentServiceList()
//         {
//             Load();
//         }

//         // load data from local json store
//         private void Load()
//         {
//             try {
//                 string students = File.ReadAllText(STUDENT_STORE);  
//                 string users = File.ReadAllText(USER_STORE);            
//                 Students = JsonSerializer.Deserialize<List<Student>>(students); 
//                 // ensure each student ticket Student property is set as this is lost in serialisation
//                 foreach(var s in Students)
//                 {
//                     foreach(var t in s.Tickets) {
//                         t.Student = s;
//                     }
//                 }            
//                 Users = JsonSerializer.Deserialize<List<User>>(users);
//             } 
//             catch (Exception )
//             {
//                 Students = new List<Student>();
            
//                 Users = new List<User>();
//             }
//         }

//         // write to local json store
//         private void Store()
//         {
//             var students = JsonSerializer.Serialize(Students);
//             File.WriteAllText(STUDENT_STORE, students);
//             var users = JsonSerializer.Serialize(Users);
//             File.WriteAllText(USER_STORE, users);
//         }

//         public void Initialise()
//         {
//             Students.Clear(); // wipe all students
//             Users.Clear();
//         }

//         // ------------------ Student Related Operations ------------------------

//         // retrieve list of Students
//         public IList<Student> GetStudents()
//         {            
//             return Students;
//         }


//         // Retrive student by Id 
//         public Student GetStudent(int id)
//         {
//             return Students.FirstOrDefault(s => s.Id == id);           
//         }

//         // Add a new student checking a student with same email does not exist
//         public Student AddStudent(string name, string course, string email, int age, double grade, string photo)
//         {
//             // check if email is already in use by another student
//             var existing = GetStudentByEmail(email);
//             if (existing != null)
//             {
//                 return null; // email in use so we cannot create student
//             } 
//             // email is unique so go ahead an create student
//             var s = new Student
//             {
//                 // simple mechanism to calculate next Id
//                 Id = Students.Count + 1,
//                 Name = name,
//                 Course = course,
//                 Email = email,
//                 Age = age,
//                 Grade = grade,
//                 PhotoUrl = photo
//             };
//             Students.Add(s);
//             Store(); // write to local file store
//             return s; // return newly added student
//         }

//         // Delete the student identified by Id returning true if deleted and false if not found
//         public bool DeleteStudent(int id)
//         {
//             var s = GetStudent(id);
//             if (s == null)
//             {
//                 return false;
//             }
//             Students.Remove(s);
//             Store(); // write to local file store
//             return true;
//         }

//         // Update the student with the details in updated 
//         public Student UpdateStudent(Student updated)
//         {
//             // verify the student exists
//             var student = GetStudent(updated.Id);
//             if (student == null)
//             {
//                 return null;
//             }
//             // update the details of the student retrieved and save
//             student.Name = updated.Name;
//             student.Email = updated.Email;
//             student.Course = updated.Course;
//             student.Age = updated.Age;
//             student.Grade = updated.Grade;

//             Store(); // write to local file store
//             return student;
//         }

//         public Student GetStudentByEmail(string email)
//         {
//             return Students.FirstOrDefault(s => s.Email == email);
//         }

//         public IList<Student> GetStudentsQuery(Func<Student,bool> q)
//         {
//             return Students.Where(q).ToList();
//         }        

//         // Miscellaneous
//         public bool IsDuplicateEmail(string email, int studentId) 
//         {
//             var existing = GetStudentByEmail(email);
//             // if a student with email exists and the Id does not match
//             // the studentId (if provided), then they cannot use the email
//             return existing != null && studentId != existing.Id;           
//         }

//         // =================== Ticket Management ===================
//         public Ticket CreateTicket(int studentId, string issue)
//         {
//             var student = GetStudent(studentId);
//             if (student == null) return null;

//             var ticket = new Ticket
//             {
//                 Id = Students.Sum(s => s.Tickets.Count()) + 1,
//                 Issue = issue,
//                 CreatedOn = DateTime.Now,
//                 Active = true,
//                 StudentId = studentId, 
//                 Student = student             
//             };
//             student.Tickets.Add(ticket);
//             Store();
//             return ticket;
//         }

//         // retrive ticket by id
//         public Ticket GetTicket(int id)
//         {
//             return Students.SelectMany(s => s.Tickets)
//                            .FirstOrDefault(t => t.Id == id);
//         }

//         // close specified ticket
//         public Ticket CloseTicket(int id, string resolution)
//         {
//             var ticket = GetTicket(id);
//             if (ticket == null || !ticket.Active) return null;
            
//             ticket.Active = false;
//             ticket.ResolvedOn = DateTime.Now;
//             ticket.Resolution = resolution;
//             Store();
            
//             return ticket;
//         }

//         // remove specified ticket
//         public bool DeleteTicket(int id)
//         {
//             // find ticket
//             var ticket = GetTicket(id);
//             if (ticket == null) return false;
            
//             // remove ticket from student
//             var result = ticket.Student.Tickets.Remove(ticket);
//             Store();
//             return result;
//         }

//         // retrieve all tickets
//         public IList<Ticket> GetAllTickets()
//         {
//             var tickets = Students.SelectMany(s => s.Tickets).ToList();
//             return tickets;
//         }

//         // retrieve only open (active) tickets
//         public IList<Ticket> GetOpenTickets()
//         {
//             var tickets = Students.SelectMany(s => s.Tickets.Where(t => t.Active)).ToList();
//             return tickets;
//         }

//          // perform search on ticket
//         public IList<Ticket> SearchTickets(TicketRange range, string query)
//         {
//             query = query == null ? "" : query.ToLower();
//             //range = range == null ? "ALL" : range.ToUpper();
            
//             // search ticket student name
//             var r1 = Students.SelectMany(s => s.Tickets).Where(t => GetStudent(t.StudentId).Name.ToLower().Contains(query.ToLower()));

//             // search ticket issue
//             var r2 = Students.SelectMany(s => s.Tickets)
//                              .Where(t => t.Issue.ToLower().Contains(query.ToLower()));
           
//             // execute the join Query (calling ToList() executes the query)
//            var r = r1.Union(r2).Where(t => 
//                     range == TicketRange.OPEN && t.Active ||
//                     range == TicketRange.CLOSED && !t.Active ||
//                     range == TicketRange.ALL
//             ).ToList();

//             return r;
      
//         }
//         public IList<Ticket> GetTicketsQuery(Func<Ticket,bool> q)
//         {
//             return  Students.SelectMany(s => s.Tickets)                    
//                             .Where(q)
//                             .ToList();
//         }
     

//         // ------------------ User Related Operations ------------------------

//         // Retrive User by email 
//         public User GetUserByEmail(string email)
//         {
//             return Users.FirstOrDefault(u => u.Email == email);
//         }

//         // Authenticate a user
//         public User Authenticate(string email, string password)
//         {
//             // retrieve the user based on the Email address (assumes Email is unique)
//             var user = GetUserByEmail(email);

//             // Verify the user exists password matches the password provided
//             if (user == null || user.Password != password)
//             {
//                 return null; // no such user
//             }
//             return user; // user authenticated
//         }

      
//         // Register a new user
//         public User Register(string name, string email, string password, Role role)
//         {
//             // check that the user does not already exist (unique user name)
//             var exists = GetUserByEmail(email);
//             if (exists != null)
//             {
//                 return null;
//             }

//             // create user
//             var user = new User 
//             {
//                 Name = name,
//                 Email = email,
//                 Password = password,
//                 Role = role   
//             };
                
//             Users.Add(user);
//             Store();
//             return user;
//         }

      
//     }
// }