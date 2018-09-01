using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Splitwise_Backend.Models
{
    
    public class Friend
    {
        [Key]
        public int FriendId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RegistrationStatus { get; set; }
        public IEnumerable<Picture> Picture { get; set; }
        public IEnumerable<Group> Group { get; set; }
        public int Balance { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
