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
    [Route("api/Friends")]
    public class FriendsController : Controller
    {
        private readonly Splitwise_BackendContext _context;

        public FriendsController(Splitwise_BackendContext context)
        {
            _context = context;
        }

        // GET: api/Friends
        [HttpGet]
        public IEnumerable<Friend> GetFriend()
        {
            var friend = _context.Friend
                 .Include("Picture")
                 .Select(f => new Friend
                 {
                     FriendId = f.FriendId,
                     FirstName = f.FirstName,
                     LastName = f.LastName,
                     Email = f.Email,
                     Balance = f.Balance,
                     RegistrationStatus = f.RegistrationStatus,
                     UpdatedAt = f.UpdatedAt,
                     Group = f.Group.Select(g => new Group
                     {
                         GroupId = g.GroupId,
                         GroupName = g.GroupName,
                         CountryCode = g.CountryCode,
                         UpdatedAt = g.UpdatedAt,
                     }),
                     Picture = f.Picture.Select(p => new Picture
                     {
                         Picture_Id = p.Picture_Id,
                         Small = p.Small,
                         Large = p.Large,
                         Medium = p.Medium
                     })
                 });
            return friend.ToList();
        }

        // GET: api/Friends/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFriend([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var friend = await _context.Friend.SingleOrDefaultAsync(m => m.FriendId == id);
            var friend = _context.Friend
                .Where(f => f.FriendId == id)
                .Include("Picture")
                .Select(f => new Friend
                {
                    FriendId = f.FriendId,
                    FirstName = f.FirstName,
                    LastName = f.LastName,
                    Email = f.Email,
                    Balance = f.Balance,
                    RegistrationStatus = f.RegistrationStatus,
                    UpdatedAt = f.UpdatedAt,
                    Group = f.Group.Select(g => new Group
                    {
                        GroupId = g.GroupId,
                        GroupName = g.GroupName,
                        CountryCode = g.CountryCode,
                        UpdatedAt = g.UpdatedAt,
                    }),
                    Picture = f.Picture.Select(p => new Picture
                    {
                        Picture_Id = p.Picture_Id,
                        Small = p.Small,
                        Large = p.Large,
                        Medium = p.Medium
                    })
                });

            if (friend == null)
            {
                return NotFound();
            }

            return Ok(friend);
        }

        // PUT: api/Friends/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriend([FromRoute] int id, [FromBody] Friend friend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != friend.FriendId)
            {
                return BadRequest();
            }

            _context.Entry(friend).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendExists(id))
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

        // POST: api/Friends
        [HttpPost]
        public async Task<IActionResult> PostFriend([FromBody] Friend friend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Friend.Add(friend);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriend", new { id = friend.FriendId }, friend);
        }

        // DELETE: api/Friends/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriend([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var friend = await _context.Friend.SingleOrDefaultAsync(m => m.FriendId == id);
            if (friend == null)
            {
                return NotFound();
            }

            _context.Friend.Remove(friend);
            await _context.SaveChangesAsync();

            return Ok(friend);
        }

        private bool FriendExists(int id)
        {
            return _context.Friend.Any(e => e.FriendId == id);
        }
    }
}