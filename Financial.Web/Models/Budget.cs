using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Financial.Web.Models
{
    public class Budget
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}