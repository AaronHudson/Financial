using Financial.Web.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Financial.Web.Models
{
    [NotMapped]
    public class TransactionVM
    {
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}