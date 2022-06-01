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
    public class GradesController: ControllerBase
    {
        private readonly DBContext _context; 
        public GradesController(DBContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Professor)]
        [HttpGet] 
        public ActionResult<List<Grade>> GetAll() 
        {     
            return _context.grades.ToList(); 
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Professor)]
        [HttpGet("{id}")] 
        public ActionResult<Grade> GetById(string id) 
        {    
            var item = _context.grades.Find(id);     
            if (item == null)    
            {         
                return NotFound();     
            }     
            return item;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Professor)]
        [HttpGet("students/{studid}")] 
        public ActionResult<IEnumerable<Grade>> GetGrades(string studid) 
        {    
            var items = _context.grades.Where(enrol => enrol.studentid.Equals(studid)).ToList();     
            if (items == null)    
            {         
                return NotFound();     
            }     
            return items;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Professor + "," + Roles.Student)]
        [HttpGet("courses/{coursId}")] 
        public ActionResult<IEnumerable<Grade>> GetGradesForCourse(string coursId) 
        {    
            var items = _context.grades.Where(enrol => enrol.courseid.Equals(coursId)).ToList();     
            if (items == null)    
            {         
                return NotFound();     
            }     
            return items;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public ActionResult<Grade> Create(Grade grade) 
        {    
            Grade newGrade = new ()
            {
                id = Guid.NewGuid().ToString(),
                studentid = grade.studentid,
                courseid = grade.courseid,
            };
            
            _context.grades.Add(newGrade);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = newGrade.id}, newGrade);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Professor)]
        [HttpPut("{id}")]
        public ActionResult Update(string id, Grade grade)
        {
            Grade existing = null;
            try
            {
                existing = _context.grades.First(s => s.id == id);
            }
            catch
            {
                if (existing is null)
                {
                    return NotFound();
                }
            }

            _context.Entry(existing).CurrentValues.SetValues(grade);
            _context.SaveChanges();

            return NoContent();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existing = _context.grades.Find(id);
            if (existing is null)
            {
                return NotFound();
            }

            _context.grades.Remove(existing);
            _context.SaveChanges();
            return NoContent();
        }
    }
}