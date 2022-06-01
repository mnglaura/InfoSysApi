using System;
using System.Collections.Generic;
using System.Linq;
using infosysapi.Auth;
using infosysapi.Context;
using infosysapi.Dtos;
using infosysapi.Extensions;
using infosysapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace infosysapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorsController: ControllerBase
    {
        private readonly DBContext _context; 
        public ProfessorsController(DBContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet] 
        public ActionResult<List<Professor>> GetAll() 
        {     
            return _context.professors.ToList(); 
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("{id}")] 
        public ActionResult<ProfessorDto> GetById(string id) 
        {    
            var item = _context.professors.Find(id);     
            if (item == null)    
            {         
                return NotFound();     
            }     
            return item.AsDto();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public ActionResult<ProfessorDto> Create(ProfessorDto prof) 
        {    
            Professor prf = new ()
            {
                id = Guid.NewGuid().ToString(),
                firstname = prof.firstname,
                lastname = prof.lastname,
                email = prof.email
            };
            
            _context.professors.Add(prf);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = prf.id}, prf.AsDto());
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public ActionResult Update(string id, ProfessorDto prof)
        {
            Professor existing = null;
            try
            {
                existing = _context.professors.First(s => s.id == id);
            }
            catch
            {
                if (existing is null)
                {
                    return NotFound();
                }
            }

            _context.Entry(existing).CurrentValues.SetValues(prof);
            _context.SaveChanges();

            return NoContent();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existing = _context.professors.Find(id);
            if (existing is null)
            {
                return NotFound();
            }

            _context.professors.Remove(existing);
            _context.SaveChanges();
            return NoContent();
        }
    }
}