using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [RegularExpression(@"^(\$?\d{1,3}(,?\d{3})?(\.\d\d?)?|\(\$?\d{1,3}(,?\d{3})?(\.\d\d?)?\))$", ErrorMessage ="I'm sorry, this doesn't appear to match a US currency.")]
        [DisplayName("Goal")]
        public string Limit { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int BudgetId { get; set; }
    }
}