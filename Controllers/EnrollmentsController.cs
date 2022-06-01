using System;
using System.Collections.Generic;
using System.Linq;
using infosysapi.Context;
using infosysapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace infosysapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController: ControllerBase
    {
        private readonly DBContext _context; 
        public EnrollmentsController(DBContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public ActionResult<List<StudEnrollment>> GetAll() 
        {     
            return _context.studenrollments.ToList(); 
        }

        [HttpGet("{id}")] 
        public ActionResult<StudEnrollment> GetById(string id) 
        {    
            var item = _context.studenrollments.Find(id);     
            if (item == null)    
            {         
                return NotFound();     
            }     
            return item;
        }

        [HttpGet("students/{studentId}")] 
        public ActionResult<IEnumerable<StudEnrollment>> GetCoursesForStudent(string studentId) 
        {    
            var items = _context.studenrollments.Where(enrol => enrol.studentid.Equals(studentId)).ToList();     
            if (items == null)    
            {         
                return NotFound();     
            }     
            return items;
        }

        [HttpGet("courses/{coursId}")] 
        public ActionResult<IEnumerable<StudEnrollment>> GetStudentsEnrolled(string coursId) 
        {    
            var items = _context.studenrollments.Where(enrol => enrol.courseid.Equals(coursId)).ToList();     
            if (items == null)    
            {         
                return NotFound();     
            }     
            return items;
        }

        [HttpPost]
        public ActionResult<StudEnrollment> Create(StudEnrollment enrol) 
        {    
            StudEnrollment newEnrl = new ()
            {
                id = Guid.NewGuid().ToString(),
                studentid = enrol.studentid,
                courseid = enrol.courseid,
            };
            
            _context.studenrollments.Add(newEnrl);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = newEnrl.id}, newEnrl);
        }

        [HttpPut("{id}")]
        public ActionResult Update(string id, StudEnrollment cours)
        {
            StudEnrollment existing = null;
            try
            {
                existing = _context.studenrollments.First(s => s.id == id);
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
            var existing = _context.studenrollments.Find(id);
            if (existing is null)
            {
                return NotFound();
            }

            _context.studenrollments.Remove(existing);
            _context.SaveChanges();
            return NoContent();
        }
    }
}