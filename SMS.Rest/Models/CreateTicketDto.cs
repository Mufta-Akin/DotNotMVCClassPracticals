using System;
using System.ComponentModel.DataAnnotations;
using SMS.Data.Models;

namespace SMS.Rest.Dtos
{
    public class CreateTicketDto 
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public string Issue { get; set; }
    }
}