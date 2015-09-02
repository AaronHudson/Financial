using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financial.Web.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public List<Transaction> Transactions { get; set; }
        public int Limit { get; set; }
        public FinancialtronUser User { get; set; }
        public string Title { get; set; }
    }
}