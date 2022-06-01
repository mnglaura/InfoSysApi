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
    public class GradesController: ControllerBase
    {
        private readonly DBContext _context; 
        public GradesController(DBContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public ActionResult<List<Grade>> GetAll() 
        {     
            return _context.grades.ToList(); 
        }

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