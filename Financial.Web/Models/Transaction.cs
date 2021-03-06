﻿using System;
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
        [RegularExpression(@"^(\$?\d{1,3}(,?\d{3})?(\.\d\d?)?|\(\$?\d{1,3}(,?\d{3})?(\.\d\d?)?\))$", ErrorMessage = "I'm sorry, this doesn't seem to match a US currency.")]
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