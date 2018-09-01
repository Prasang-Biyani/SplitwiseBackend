using System.ComponentModel.DataAnnotations;
namespace Splitwise_Backend.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Balance { get; set; }
    }
}
