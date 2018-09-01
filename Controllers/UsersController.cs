using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Splitwise_Backend.Models;

namespace Splitwise_Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly Splitwise_BackendContext _context;

        public UsersController(Splitwise_BackendContext context)
        {
            _context = context;
        }


        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            var users = _context
                .User
                .Include("Picture")
                .Select(t => new User
                {
                    UserId = t.UserId,
                    Email = t.Email,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Registration = t.Registration,
                    Picture = t.Picture.Select(p => new Picture
                    {
                        Picture_Id = p.Picture_Id,
                        Small = p.Small,
                        Medium = p.Medium,
                        Large = p.Large
                    }),
                    
                   

                }).ToList();
            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var user = await _context.User.SingleOrDefaultAsync(m => m.UserId == id);

            var user = _context.User
                .Where(u => u.UserId == id)
                .Include("Picture")
                .Select(u => new User
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Registration = u.Registration,
                    Picture = u.Picture.Select(p => new Picture
                    {
                        Picture_Id = p.Picture_Id,
                        Small = p.Small,
                        Medium = p.Medium,
                        Large = p.Large
                    })
                });

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }
}