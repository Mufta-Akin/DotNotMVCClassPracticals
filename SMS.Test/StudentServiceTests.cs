using System;
using Xunit;
using SMS.Data.Services;
using SMS.Data.Models;

namespace SMS.Test
{
    public class StudentServiceTest
    {
        private readonly IStudentService svc;

        public StudentServiceTest()
        {
            // general arrangement
            svc = new StudentServiceDb();

            // ensure data source is empty before each test
            svc.Initialise();
        }

        [Fact] 
        public void Student_GetStudentsWhenNone_ShouldReturnNone()
        {
            // arrange

            // act 
            var students = svc.GetStudents();
            var count = students.Count;

            // assert
            Assert.Equal(0, count);
        }

        [Fact]
        public void Student_AddStudentWhenUnique_ShouldSetAllProperties()
        {
            // arrange

            // act 
            var o = svc.AddStudent("XXX", "Computing", "xxx@email.com", 20, 0, "https://photo.com");            
            var s = svc.GetStudent(o.Id); // retrieve student saved 

            // assert - that student is not null
            Assert.NotNull(s);
            Assert.Equal(s.Id, s.Id);
            Assert.Equal("XXX", s.Name);
            Assert.Equal("xxx@email.com", s.Email);
            Assert.Equal("Computing", s.Course);
            Assert.Equal(20, s.Age);
            Assert.Equal(0, s.Grade);
            Assert.Equal("https://photo.com", s.PhotoUrl);
        }

        [Fact] 
        public void Student_AddWhenDuplicateEmail_ShouldReturnNull()
        {
            // arrange 
            var s1 = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0, "https://photo.com");

            // act - add duplicate
            var s2 = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0, "https://photo.com");

            // assert
            Assert.NotNull(s1);
            Assert.Null(s2);
        }

        [Fact]
        public void Student_UpdateWhenExists_ShouldSetAllProperties()
        {
            // arrange - create test student
            var o = svc.AddStudent("ZZZ", "zzz@email.com",  "Maths", 30, 100, "https://photo.com");
                       
            // act - update test student         
            o.Name = "XXX"; 
            o.Email = "xxx@email.com";         
            o.Course = "Computing";
            o.Age = o.Age = 31;
            o.Grade = 50;
            o.PhotoUrl = "https://xxx.com" ;
       
            o = svc.UpdateStudent(o);           
            
            // assert
            Assert.NotNull(o);                 
            Assert.Equal("XXX", o.Name);
            Assert.Equal("xxx@email.com", o.Email);
            Assert.Equal("Computing", o.Course);
            Assert.Equal(31, o.Age);
            Assert.Equal(50, o.Grade);
            Assert.Equal("https://xxx.com", o.PhotoUrl);
        }
 

        [Fact]
        public void Student_GetStudentsWhenTwoAdded_ShouldReturnTwo()
        {
            // arrange
            var s1 = svc.AddStudent("XXX", "Computing",   "xxx@email.com", 20, 0, "https://photo.com");
            var s2 = svc.AddStudent("YYY", "Engineering", "yyy@email.com", 23, 0, "https://photo.com");

            // act
            var students = svc.GetStudents();
            var count = students.Count;

            // assert
            Assert.Equal(2, count);
        }

        [Fact] 
        public void Student_GetStudentWhenNone_ShouldReturnNull()
        {
            // arrange

            // act 
            var student = svc.GetStudent(1); // non existent student

            // assert
            Assert.Null(student);
        }

        [Fact] 
        public void Student_GetStudentThatExists_ShouldReturnStudent()
        {
            // arrange 
            var s = svc.AddStudent("XXX", "Computing", "xxx@email.com", 20, 0, "https://photo.com");

            // act
            var ns = svc.GetStudent(s.Id);

            // assert
            Assert.NotNull(ns);
            Assert.Equal(s.Id, ns.Id);
        }


        [Fact]
        public void Student_DeleteStudentThatExists_ShouldReturnTrue()
        {
            // arrange 
            var s = svc.AddStudent("XXX", "Computing", "xxx@email.com", 20, 0, "https://photo.com");
            
            // act
            var deleted = svc.DeleteStudent(s.Id);  
            var s1 = svc.GetStudent(s.Id);           // try to retrieve deleted student

            // assert
            Assert.True(deleted); // delete student should return true
            Assert.Null(s1);      // s1 should be null
        }

         [Fact]
        public void Student_DeleteStudentThatExists_ShouldReduceStudentCountByOne()
        {
            // arrange
            var s = svc.AddStudent("XXX", "Computing", "xxx@email.com", 20, 0, "https://photo.com");
            
            //act
            var deleted = svc.DeleteStudent(s.Id);
            var students = svc.GetStudents();

            // assert
            Assert.Equal(0, students.Count);      // should be 0 students
        }


        [Fact]
        public void Student_DeleteStudentThatDoesntExist_ShouldNotChangeStudentCount()
        {
            // arrange
            var s = svc.AddStudent("XXX", "Computing", "xxx@email.com", 20, 0, "https://photo.com");
         
            // act 	
            svc.DeleteStudent(0);               // delete non existent student
            var students = svc.GetStudents();   // retrieve list of students

            // assert
            Assert.Equal(1, students.Count);    // should be 1 student
        }        

        // ---------------------- Ticket Tests ------------------------
        
        [Fact] // --- AddTicket should be Active
        public void Ticket_CreateTicketForExistingStudent_ShouldBeActive()
        {
            // arrange
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0, "http://photo.com");
         
            // act
            var t = svc.CreateTicket(s.Id, "Dummy Ticket 1");
           
            // assert
            Assert.True(t.Active); 
        }

        [Fact] // --- GetOpenTickets When two added should return two 
        public void Ticket_GetOpenTicketsWhenTwoAdded_ShouldReturnTwo()
        {
            // arrange
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0, "http://photo.com");
            var t1 = svc.CreateTicket(s.Id, "Dummy Ticket 1");
            var t2 = svc.CreateTicket(s.Id, "Dummy Ticket 2");

            // act
            var open = svc.GetOpenTickets();

            // assert
            Assert.Equal(2,open.Count);                        
        }

        [Fact] 
        public void Ticket_CloseTicketWhenOpen_ShouldReturnTicket()
        {
            // arrange
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0, "http://photo.com");
            var t = svc.CreateTicket(s.Id, "Dummy Ticket");

            // act
            var r = svc.CloseTicket(t.Id);

            // assert
            Assert.NotNull(r);              // verify closed ticket returned          
            Assert.False(r.Active);
        }

        [Fact] 
        public void Ticket_CloseTicketWhenAlreadyClosed_ShouldReturnNull()
        {
            // arrange
            var s = svc.AddStudent("XXX", "xxx@email.com", "Computing", 20, 0, "http://photo.com");
            var t = svc.CreateTicket(s.Id, "Dummy Ticket");

            // act
            var closed = svc.CloseTicket(t.Id);     // close active ticket    
            closed = svc.CloseTicket(t.Id);         // close non active ticket

            // assert
            Assert.Null(closed);                    // no ticket returned as already closed
        }

        //  =================  User Tests ===========================
        
        [Fact] // --- Register Valid User test
        public void User_Register_WhenValid_ShouldReturnUser()
        {
            // arrange 
            var reg = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);
            
            // act
            var user = svc.GetUserByEmail(reg.Email);
            
            // assert
            Assert.NotNull(reg);
            Assert.NotNull(user);
        } 

        [Fact] // --- Register Duplicate Test
        public void User_Register_WhenDuplicateEmail_ShouldReturnNull()
        {
            // arrange 
            var s1 = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);
            
            // act
            var s2 = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);

            // assert
            Assert.NotNull(s1);
            Assert.Null(s2);
        } 

        [Fact] // --- Authenticate Invalid Test
        public void User_Authenticate_WhenInValidCredentials_ShouldReturnNull()
        {
            // arrange 
            var s1 = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);
        
            // act
            var user = svc.Authenticate("xxx@email.com", "guest");
            // assert
            Assert.Null(user);

        } 

        [Fact] // --- Authenticate Valid Test
        public void User_Authenticate_WhenValidCredentials_ShouldReturnUser()
        {
            // arrange 
            var s1 = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);
        
            // act
            var user = svc.Authenticate("xxx@email.com", "admin");
            
            // assert
            Assert.NotNull(user);
        } 

        
    }
}
