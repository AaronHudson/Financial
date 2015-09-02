using System;

namespace Financial.Web.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Amount { get; set; }
        public Budget Budget { get; set; }
        public FinancialtronUser User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}