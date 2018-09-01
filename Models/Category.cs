using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Splitwise_Backend.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public IEnumerable<SubCategory> Sub { get; set; }
    }

    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
    }
}
