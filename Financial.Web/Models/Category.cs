using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Financial.Web.Models
{
    public class Category
    {
        public int Id { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
        public decimal Limit { get; set; }
        public ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public decimal Remainder
        {
            get
            {
                decimal sum = 0;
                foreach (Transaction transaction in Transactions)
                {
                    sum += transaction.Amount;
                }
                return Limit - sum;
            }
        }
    }
    }