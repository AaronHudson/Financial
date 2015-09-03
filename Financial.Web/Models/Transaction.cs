using System;

namespace Financial.Web.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Amount { get; set; }
        public Category Budget { get; set; }
        public ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}