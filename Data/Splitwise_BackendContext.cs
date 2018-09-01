using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Splitwise_Backend.Models;

namespace Splitwise_Backend.Models
{
    public class Splitwise_BackendContext : DbContext
    {
        public Splitwise_BackendContext (DbContextOptions<Splitwise_BackendContext> options)
            : base(options)
        {
        }

        public DbSet<Splitwise_Backend.Models.User> User { get; set; }

        public DbSet<Splitwise_Backend.Models.Group> Group { get; set; }

        public DbSet<Splitwise_Backend.Models.Friend> Friend { get; set; }

        public DbSet<Splitwise_Backend.Models.Expense> Expense { get; set; }
    }
}
