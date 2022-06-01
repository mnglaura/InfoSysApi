using System;
using System.Collections.Generic;
using System.Linq;
using infosysapi.Context;
using infosysapi.Dtos;
using infosysapi.Extensions;
using infosysapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace infosysapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController: ControllerBase
    {
        private readonly DBContext _context; 
        public StudentsController(DBContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public ActionResult<List<Student>> GetAll() 
        {     
            return _context.students.ToList(); 
        }

        [HttpGet("{id}")] 
        public ActionResult<StudentDto> GetStudent(string id) 
        {    
            var item = _context.students.Find(id);     
            if (item == null)    
            {         
                return NotFound();     
            }     
            return item.AsDto();
        }

        [HttpPost]
        public ActionResult<StudentDto> CreateStudent(StudentDto student) 
        {    
            Student stud = new ()
            {
                id = Guid.NewGuid().ToString(),
                firstname = student.firstname,
                lastname = student.lastname,
                email = student.email
            };
            
            _context.students.Add(stud);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetStudent), new {id = stud.id}, stud.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateStudent(string id, StudentDto student)
        {
            var existing = _context.students.First(s => s.id == id);
            if (existing is null)
            {
                return NotFound();
            }

            _context.Entry(existing).CurrentValues.SetValues(student);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existing = _context.students.Find(id);
            if (existing is null)
            {
                return NotFound();
            }

            _context.students.Remove(existing);
            _context.SaveChanges();
            return NoContent();
        }
    }
}