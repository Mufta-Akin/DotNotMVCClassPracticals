using System;
using System.Text;
using System.Collections.Generic;
using SMS.Data.Models;

namespace SMS.Data.Services
{
    public static class ServiceSeeder
    {

        // use this class to seed the database with dummy 
        // test data using an IStudentService 
        public static void Seed(IStudentService svc)
        {
            svc.Initialise();
            
            // add students
            var s1 = svc.AddStudent("Homer Simpson","Physics", "homer@mail.com", 41, 56, "https://static.wikia.nocookie.net/simpsons/images/b/bd/Homer_Simpson.png" );
            var s2 = svc.AddStudent("Marge Simpson","English", "marge@mail.com", 38, 69 , "https://static.wikia.nocookie.net/simpsons/images/4/4d/MargeSimpson.png");
            var s3 = svc.AddStudent("Bart Simpson","Maths", "bart@mail.com", 14, 61, "https://upload.wikimedia.org/wikipedia/en/a/aa/Bart_Simpson_200px.png" );
            var s4 = svc.AddStudent("Lisa Simpson","Poetry", "lisa@mail.com", 13, 80, "https://upload.wikimedia.org/wikipedia/en/e/ec/Lisa_Simpson.png" );
            var s5 = svc.AddStudent("Mr Burns","Management", "burns@mail.com", 81, 63, "https://static.wikia.nocookie.net/simpsons/images/a/a7/Montgomery_Burns.png" );
           
            // add tickets
            svc.CreateTicket(s1.Id, "Why Bart you little......");
            svc.CreateTicket(s1.Id, "What button do I press...");
            svc.CreateTicket(s2.Id, "Reset my password .......");
            svc.CreateTicket(s5.Id, "How do I sack Homer......");

            // Q4 - add users
            // Call service Register method to add 3 users (one for each role)
            // email/password 
            // admin@sms.com/admin, manager@sms.com/manager, guest@sms.com/guest

        }
    }
}
