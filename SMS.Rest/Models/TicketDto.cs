using System;
using System.ComponentModel.DataAnnotations;
using SMS.Data.Models;

namespace SMS.Rest.Dtos
{

    public class TicketDto 
    {
        public int Id { get; set;}

        [Required]
        public int StudentId { get; set; }
        [Required]
        public string Issue { get; set; }
        
        public string Resolution { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ResolvedOn { get; set; }
        public bool Active { get; set; }

        public static Ticket ToTicket(TicketDto t)
        {
           return new Ticket {
                Id = t.Id,
                StudentId = t.StudentId,
                Issue = t.Issue,
                CreatedOn = t.CreatedOn,
                Active = t.Active
            };
        }
        
        public static TicketDto FromTicket(Ticket t)
        {
            return new TicketDto {
                Id = t.Id,
                StudentId = t.StudentId,
                Issue = t.Issue,
                CreatedOn = t.CreatedOn,
                Active = t.Active
            };
        }            
    }
}
