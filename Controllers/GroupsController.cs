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
    [Route("api/Groups")]
    public class GroupsController : Controller
    {
        private readonly Splitwise_BackendContext _context;

        public GroupsController(Splitwise_BackendContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet]
        public IEnumerable<Group> GetGroup()
        {
            var group = _context
                .Group
                .Include("Member")
                .Select(g => new Group
                {
                    GroupId = g.GroupId,
                    GroupName = g.GroupName,
                    CountryCode = g.CountryCode,
                    UpdatedAt = g.UpdatedAt,
                    Member = g.Member.Select(m => new Member
                    {
                       MemberId = m.MemberId,
                       FirstName = m.FirstName,
                       LastName = m.LastName,
                       Email = m.Email
                    }),
                    Payment = g.Payment.Select(p => new Payment
                    {
                        PaymentId = p.PaymentId,
                        From = p.From,
                        To = p.To,
                        Amount = p.Amount
                    }) 
                });

            return group.ToList();
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var @group = await _context.Group.SingleOrDefaultAsync(m => m.GroupId == id);
                var group = _context
                .Group.Where(u => u.GroupId == id)
                .Include("Member")
                .Include("Payment")
                .Select(g => new Group
                {
                    GroupId = g.GroupId,
                    GroupName = g.GroupName,
                    CountryCode = g.CountryCode,
                    UpdatedAt = g.UpdatedAt,
                    Member = g.Member.Select(m => new Member
                    {
                        Balance = m.Balance,
                        Email = m.Email,
                        FirstName = m.FirstName,
                        LastName = m.LastName,
                        MemberId = m.MemberId
                    }),
                    Payment = g.Payment.Select(p => new Payment
                    {
                        From = p.From,
                        To = p.To,
                        Amount = p.Amount,
                        PaymentId = p.PaymentId
                    })
                });

            if (@group == null)
            {
                return NotFound();
            }

            return Ok(@group);
        }

        // PUT: api/Groups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup([FromRoute] int id, [FromBody] Group @group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @group.GroupId)
            {
                return BadRequest();
            }

            _context.Entry(@group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        [HttpPost]
        public async Task<IActionResult> PostGroup([FromBody] Group @group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Group.Add(@group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = @group.GroupId }, @group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @group = await _context.Group.SingleOrDefaultAsync(m => m.GroupId == id);
            if (@group == null)
            {
                return NotFound();
            }

            _context.Group.Remove(@group);
            await _context.SaveChangesAsync();

            return Ok(@group);
        }

        private bool GroupExists(int id)
        {
            return _context.Group.Any(e => e.GroupId == id);
        }
    }
}