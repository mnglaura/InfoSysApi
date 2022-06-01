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
    public class HomeworksController: ControllerBase
    {
        private readonly DBContext _context; 
        public HomeworksController(DBContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Student)]
        [HttpGet] 
        public ActionResult<List<Homework>> GetAll() 
        {     
            return _context.homeworks.ToList(); 
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Student)]
        [HttpGet("{id}")] 
        public ActionResult<Homework> GetById(string id) 
        {    
            var item = _context.homeworks.Find(id);     
            if (item == null)    
            {         
                return NotFound();     
            }     
            return item;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Student)]
        [HttpGet("students/{studid}")] 
        public ActionResult<IEnumerable<Homework>> GetHomeworksForStudent(string studid) 
        {    
            var items = _context.homeworks.Where(enrol => enrol.studentid.Equals(studid)).ToList();     
            if (items == null)    
            {         
                return NotFound();     
            }     
            return items;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Professor)]
        [HttpGet("courses/{coursId}")] 
        public ActionResult<IEnumerable<Homework>> GetHomeworksForCourse(string coursId) 
        {    
            var items = _context.homeworks.Where(enrol => enrol.courseid.Equals(coursId)).ToList();     
            if (items == null)    
            {         
                return NotFound();     
            }     
            return items;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Student)]
        [HttpGet("status")] 
        public ActionResult<IEnumerable<Homework>> GetByStatus(string status) 
        {    
            var items = _context.homeworks.Where(enrol => enrol.status.Equals(status)).ToList();     
            if (items == null)    
            {         
                return NotFound();     
            }     
            return items;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Student)]
        [HttpPost]
        public ActionResult<Homework> Create(Homework hmk) 
        {    
            Homework newGrade = new ()
            {
                id = Guid.NewGuid().ToString(),
                studentid = hmk.studentid,
                courseid = hmk.courseid,
                status = hmk.status,
                submissiondate = hmk.submissiondate
            };
            
            _context.homeworks.Add(newGrade);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = newGrade.id}, newGrade);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Student)]
        [HttpPut("{id}")]
        public ActionResult Update(string id, Homework hmk)
        {
            Homework existing = null;
            try
            {
                existing = _context.homeworks.First(s => s.id == id);
            }
            catch
            {
                if (existing is null)
                {
                    return NotFound();
                }
            }

            _context.Entry(existing).CurrentValues.SetValues(hmk);
            _context.SaveChanges();

            return NoContent();
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Student)]
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existing = _context.homeworks.Find(id);
            if (existing is null)
            {
                return NotFound();
            }

            _context.homeworks.Remove(existing);
            _context.SaveChanges();
            return NoContent();
        }
    }
}