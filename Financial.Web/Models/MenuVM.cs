using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Financial.Web.Models
{
    [NotMapped]
    public class MenuVM
    {
        public Dictionary<string, int> Budgets { get; set; }
        public Dictionary<string, int> Categories { get; set; }
        public Dictionary<string, int> Transactions { get; set; }

        public int CurrentBudget { get; set; }
        public int CurrentCategory { get; set; }

        public bool HasCategories { get; set; }
        public bool HasTransactions { get; set; }

        public MenuVM()
        {
            Budgets = new Dictionary<string, int>();
            HasCategories = false;
            HasTransactions = false;
        }
    }
}