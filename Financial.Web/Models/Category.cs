using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Financial.Web.Models
{
    public class Category
    {
        public int Id { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        [ForeignKey("Budget")]
        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }
        [Required]
        [RegularExpression(@"^(\$?\d{1,3}(,?\d{3})?(\.\d\d?)?|\(\$?\d{1,3}(,?\d{3})?(\.\d\d?)?\))$", ErrorMessage = "I'm sorry, this doesn't seem to match a US currency.")]
        [DisplayName("Goal")]
        public decimal Limit { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Balance
        {
            get
            {
                decimal sum = 0;
                if (Transactions != null)
                foreach (Transaction transaction in Transactions)
                {
                    sum += transaction.Amount;
                }
                return sum;
            }
        }
    }
    }