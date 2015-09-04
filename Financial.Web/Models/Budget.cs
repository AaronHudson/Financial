using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financial.Web.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual List<Category> Categories { get; set; }
        public ApplicationUser User { get; set; }
    }
}