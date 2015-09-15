using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Financial.Web.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        [RegularExpression(@"^[+-]?[0-9]{1,3}(?:,?[0-9]{3})*(?:\.[0-9]{2})?$")]
        public decimal Amount { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Required]
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