using System.ComponentModel.DataAnnotations;

namespace Splitwise_Backend.Models
{
    public class Picture
    {
        [Key]
        public int Picture_Id { get; set; }
        public string Small { get; set; }
        public string Medium { get; set; }
        public string Large { get; set; }
    }
}
