using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Splitwise_Backend.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string CountryCode { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public IEnumerable<Member> Member { get; set; }
        public IEnumerable<Payment> Payment { get; set; }
        
    }
}
