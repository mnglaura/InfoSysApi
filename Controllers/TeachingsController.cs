using System;
using System.Collections.Generic;
using System.Linq;
using infosysapi.Auth;
using infosysapi.Context;
using infosysapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace infosysapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeachingsController: ControllerBase
    {
        private readonly DBContext _context; 
        public TeachingsController(DBContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Professor)]
        [HttpGet] 
        public ActionResult<List<ProfTeaching>> GetAll() 
        {     
            return _context.profteachings.ToList(); 
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Professor)]
        [HttpGet("{id}")] 
        public ActionResult<ProfTeaching> GetById(string id) 
        {    
            var item = _context.profteachings.Find(id);     
            if (item == null)    
            {         
                return NotFound();     
            }     
            return item;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Professor)]
        [HttpGet("professors/{profid}")] 
        public ActionResult<IEnumerable<ProfTeaching>> GetCoursesForProfessor(string profid) 
        {    
            var items = _context.profteachings.Where(enrol => enrol.profid.Equals(profid)).ToList();     
            if (items == null)    
            {         
                return NotFound();     
            }     
            return items;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Professor)]
        [HttpGet("courses/{coursId}")] 
        public ActionResult<IEnumerable<ProfTeaching>> GetProfessorsTeachingCourse(string coursId) 
        {    
            var items = _context.profteachings.Where(enrol => enrol.courseid.Equals(coursId)).ToList();     
            if (items == null)    
            {         
                return NotFound();     
            }     
            return items;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Professor)]
        [HttpPost]
        public ActionResult<ProfTeaching> Create(ProfTeaching tch) 
        {    
            ProfTeaching newtch = new ()
            {
                id = Guid.NewGuid().ToString(),
                profid = tch.profid,
                courseid = tch.courseid,
            };
            
            _context.profteachings.Add(newtch);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = newtch.id}, newtch);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Professor)]
        [HttpPut("{id}")]
        public ActionResult Update(string id, ProfTeaching tch)
        {
            ProfTeaching existing = null;
            try
            {
                existing = _context.profteachings.First(s => s.id == id);
            }
            catch
            {
                if (existing is null)
                {
                    return NotFound();
                }
            }

            _context.Entry(existing).CurrentValues.SetValues(tch);
            _context.SaveChanges();

            return NoContent();
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Professor)]
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existing = _context.profteachings.Find(id);
            if (existing is null)
            {
                return NotFound();
            }

            _context.profteachings.Remove(existing);
            _context.SaveChanges();
            return NoContent();
        }
    }
}