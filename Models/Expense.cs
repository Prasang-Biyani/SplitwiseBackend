using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Splitwise_Backend.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }
        public IEnumerable<Group> Group { get; set; }
        public IEnumerable<Friend> Friend { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public int Cost { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public DateTime Deleted_At { get; set; }
        public IEnumerable<User> User { get; set; }
    }
}
