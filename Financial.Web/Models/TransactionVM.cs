using Financial.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Financial.Web.Models
{
    [NotMapped]
    public class TransactionVM
    {
        public int CategoryId { get; set; }
        [Required]
        [RegularExpression(@"^(\$?\d{1,3}(,?\d{3})?(\.\d\d?)?|\(\$?\d{1,3}(,?\d{3})?(\.\d\d?)?\))$", ErrorMessage = "I'm sorry, this doesn't seem to match a US currency.")]
        public string Amount { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public SelectList Categories { get; set; }
    }
}