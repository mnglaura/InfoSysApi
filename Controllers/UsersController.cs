using System.Collections.Generic;
using System.ComponentModel;
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
    public class UsersController : ControllerBase
    {
        private readonly IJWTManagerContext _jWTManager;
        private readonly DBContext _context; 
        public UsersController(IJWTManagerContext jWTManager, DBContext context)
        {
            this._jWTManager = jWTManager;
            this._context = context;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public List<User> Get()
        {
            return _context.users.ToList(); 
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existing = _context.users.Find(id);
            if (existing is null)
            {
                return NotFound();
            }

            _context.users.Remove(existing);
            _context.SaveChanges();
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(AuthModel authUser)
        {
            var existing = _context.users.Where(x => x.username == authUser.username && x.password == authUser.password);
            if (existing.Count() > 1)
            {
                throw new InvalidEnumArgumentException("There are more than one user with these credentials");                
            }
            // return null if user not found
            if (existing == null)
                return NotFound();

            var token = _jWTManager.Authenticate(existing.First());

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}