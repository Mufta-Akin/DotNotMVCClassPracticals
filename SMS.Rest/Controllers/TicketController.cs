using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.Data.Models;
using SMS.Data.Services;
//using SMS.Rest.Dtos;

namespace SMS.Rest.Controllers
{
    /*
    return Ok(); // Http status code 200
    return Created(); // Http status code 201
    return NoContent(); // Http status code 204
    return BadRequest(); // Http status code 400
    return Unauthorized(); // Http status code 401
    return Forbid(); // Http status code 403
    return NotFound(); // Http status code 404
    */

    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private IStudentService _service;
        public TicketController() {
            this._service = new StudentServiceDb();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var students =  _service.GetAllTickets();          
            return Ok(students);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var t =  _service.GetTicket(id); 
            if (t == null)
            {
                return NotFound();
            }
            
            return Ok(t);           
        }

        [HttpPost] 
        [Authorize(Roles="Admin")]   
        public IActionResult create(int studentId, string issue)
        {
            if (ModelState.IsValid)
            {
                var result = _service.CreateTicket(studentId,issue);
                if (result != null)
                {
                    return CreatedAtAction(nameof(Get), new { Id = result.Id }, result);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]   
        public IActionResult delete(int id)
        {
            var ok = _service.DeleteTicket(id);
            if (ok)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
