using System;
using System.Collections.Generic;

using SMS.Data.Models;

namespace SMS.Data.Services
{
    // This interface describes the operations that a StudentService class should implement
    public interface IStudentService
    {
        // Initialise the repository - only to be used during development 
        void Initialise();

        // ---------------- Student Management --------------
        IList<Student> GetStudents();
        Student GetStudent(int id);
        Student GetStudentByEmail(string email);
        bool IsDuplicateEmail(string email, int studentId);  
        Student AddStudent(string name, string course, string email, int age, double grade, string photo);
        Student UpdateStudent(Student updated);  
        bool DeleteStudent(int id);
        IList<Student> GetStudentsQuery(Func<Student,bool> q);

        // ---------------- Ticket Management ---------------
        Ticket CreateTicket(int studentId, string issue);
        Ticket GetTicket(int id);
        Ticket CloseTicket(int id);
        bool DeleteTicket(int id);
        IList<Ticket> GetAllTickets();
        IList<Ticket> GetOpenTickets();            
      
        // ------------- User Management -------------------
        User Authenticate(string email, string password);
        User Register(string name, string email, string password, Role role);
        User GetUserByEmail(string email);
        
    }
    
}
