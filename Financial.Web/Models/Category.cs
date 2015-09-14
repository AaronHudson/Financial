using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public decimal Limit { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Balance
        {
            get
            {
                decimal sum = 0;
                foreach (Transaction transaction in Transactions)
                {
                    sum += transaction.Amount;
                }
                return sum;
            }
        }
    }
    }