using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Financial.Web.Models
{
    [NotMapped]
    public class CategoryVM
    {
        [Required]
        [RegularExpression(@"^[+-]?[0-9]{1,3}(?:,?[0-9]{3})*(?:\.[0-9]{2})?$")]
        public decimal Limit { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int BudgetId { get; set; }
    }
}