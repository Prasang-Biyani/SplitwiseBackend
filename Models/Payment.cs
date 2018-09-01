using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise_Backend.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Amount { get; set; }
    }
}
