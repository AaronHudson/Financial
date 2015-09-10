using System;
using System.Collections.ObjectModel;

namespace Financial.Web.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Amount { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}