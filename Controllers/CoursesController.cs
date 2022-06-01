using System;
using System.Collections.Generic;
using System.Linq;
using infosysapi.Dtos;
using infosysapi.Extensions;
using infosysapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace infosysapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController: ControllerBase
    {
        private readonly DBContext _context; 
        public CoursesController(DBContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public ActionResult<List<Cours>> GetAll() 
        {     
            return _context.courses.ToList(); 
        }

        [HttpGet("{id}")] 
        public ActionResult<Cours> GetById(string id) 
        {    
            var item = _context.courses.Find(id);     
            if (item == null)    
            {         
                return NotFound();     
            }     
            return item;
        }

        [HttpPost]
        public ActionResult<Cours> Create(Cours cours) 
        {    
            Cours newCours = new ()
            {
                id = Guid.NewGuid().ToString(),
                name = cours.name,
                semester = cours.semester,
            };
            
            _context.courses.Add(newCours);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = newCours.id}, newCours);
        }

        [HttpPut("{id}")]
        public ActionResult Update(string id, Cours cours)
        {
            Cours existing = null;
            try
            {
                existing = _context.courses.First(s => s.id == id);
            }
            catch
            {
                if (existing is null)
                {
                    return NotFound();
                }
            }

            _context.Entry(existing).CurrentValues.SetValues(cours);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existing = _context.courses.Find(id);
            if (existing is null)
            {
                return NotFound();
            }

            _context.courses.Remove(existing);
            _context.SaveChanges();
            return NoContent();
        }
    }
}