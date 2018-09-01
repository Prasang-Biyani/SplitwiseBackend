using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Splitwise_Backend.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Registration { get; set; }
        public IEnumerable<Picture> Picture { get; set; }
    }

    
}
