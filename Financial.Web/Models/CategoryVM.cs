using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financial.Web.Models
{
    public class CategoryVM
    {
        public decimal Limit { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int BudgetId { get; set; }
    }
}