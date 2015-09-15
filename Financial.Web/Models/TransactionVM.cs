using Financial.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Financial.Web.Models
{
    [NotMapped]
    public class TransactionVM
    {
        public int CategoryId { get; set; }
        [RegularExpression(@"^[+-]?[0-9]{1,3}(?:,?[0-9]{3})*(?:\.[0-9]{2})?$")]
        public decimal Amount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}