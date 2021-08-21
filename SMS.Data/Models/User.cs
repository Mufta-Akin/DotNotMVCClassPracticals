using System;
using System.ComponentModel.DataAnnotations;

namespace SMS.Data.Models {
    
    public enum Role { admin, manager, guest }

    public class User {
        public int Id { get; set; }
       
        [Required]
        public string Name { get; set; }
       
        [Required]        
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public Role Role { get; set; }

        public string Token { get; set; }
    }
}
