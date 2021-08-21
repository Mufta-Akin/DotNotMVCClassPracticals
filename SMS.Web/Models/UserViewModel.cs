using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SMS.Data.Models;

namespace SMS.Web.Models
{
    public class UserViewModel
    {  
        // Q3 add validation attributes
        
        public string Name { get; set; } 

        public string Email { get; set; }
 
        public string Password { get; set; }

        public string PasswordConfirm  { get; set; }

        public Role Role { get; set; }

    }
}