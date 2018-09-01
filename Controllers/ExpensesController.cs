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
    [Route("api/Expenses")]
    public class ExpensesController : Controller
    {
        private readonly Splitwise_BackendContext _context;

        public ExpensesController(Splitwise_BackendContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public IEnumerable<Expense> GetExpense()
        {
            var expense = _context.Expense
                .Select(e => new Expense
                {
                    ExpenseId = e.ExpenseId,
                    Friend = e.Friend.Select(f => new Friend
                    {
                        FriendId = f.FriendId
                    }),
                    Group = e.Group.Select(g => new Group
                    {
                        GroupId = g.GroupId
                    }),
                    Description = e.Description,
                    Cost = e.Cost,
                    Date = e.Date,
                    Created_At = e.Created_At,
                    Updated_At = e.Updated_At,
                    Deleted_At = e.Deleted_At,
                    Details = e.Details,
                    Category = e.Category.Select(c => new Category
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name,
                        Sub = c.Sub.Select(s => new SubCategory
                        {
                            Name = s.Name,
                            SubCategoryId = s.SubCategoryId
                        })
                    }),
                    User = e.User.Select(u => new Models.User
                    {
                        UserId = u.UserId,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email
                    })
                });
            return expense.ToList();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var expense = await _context.Expense.SingleOrDefaultAsync(m => m.ExpenseId == id);
            var expense = _context.Expense
                .Where(e => e.ExpenseId == id)
                .Select(e => new Expense
                {
                    ExpenseId = e.ExpenseId,
                    Friend = e.Friend.Select(f => new Friend
                    {
                        FriendId = f.FriendId
                    }),
                    Group = e.Group.Select(g => new Group
                    {
                        GroupId = g.GroupId
                    }),
                    Description = e.Description,
                    Cost = e.Cost,
                    Date = e.Date,
                    Created_At = e.Created_At,
                    Updated_At = e.Updated_At,
                    Deleted_At = e.Deleted_At,
                    Details = e.Details,
                    Category = e.Category.Select(c => new Category
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name,
                        Sub = c.Sub.Select(s => new SubCategory
                        {
                            Name = s.Name,
                            SubCategoryId = s.SubCategoryId
                        })
                    }),
                    User = e.User.Select(u => new Models.User
                    {
                        UserId = u.UserId,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email
                    })
                });


            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        // PUT: api/Expenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense([FromRoute] int id, [FromBody] Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != expense.ExpenseId)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
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

        // POST: api/Expenses
        [HttpPost]
        public async Task<IActionResult> PostExpense([FromBody] Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Expense.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new { id = expense.ExpenseId }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expense = await _context.Expense.SingleOrDefaultAsync(m => m.ExpenseId == id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expense.Remove(expense);
            await _context.SaveChangesAsync();

            return Ok(expense);
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expense.Any(e => e.ExpenseId == id);
        }
    }
}