using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // required for validation annotations
using System.Text.Json.Serialization;        // required for custom json serialization options
using SMS.Data.Validators;

namespace SMS.Data.Models {

    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Course { get; set; }

        [Range(16,80)]
        public int Age { get; set; } 

        [Range(0,100)]
        public double Grade { get; set; }

        // Custom Validator 
        [UrlResource]
        public string PhotoUrl { get; set; }     
        
        public string Classification => Classify();

        // 1-N relationship      
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
        
        public override string ToString()
        {
            return $"{Id}-{Name}-{Grade}";
        }

        // private classifier function
        private string Classify()
        {
            if (Grade < 50)
            {
                return "Fail";
            }
            else if (Grade >= 50 && Grade <= 69)
            {
                return "Pass";
            }
            else if (Grade >=70 && Grade <= 79)
            {
                return "Commendation";
            }
            else
            {
                return "Distinction";
            }
        }
    }
    
}