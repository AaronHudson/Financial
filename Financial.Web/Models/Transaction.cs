using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Financial.Web.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Amount { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FormatedAmount
        {
            get
            {
                return String.Format("{0:C}", this.Amount);
            }
        }
    }
}