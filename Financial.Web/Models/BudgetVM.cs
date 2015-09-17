using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Financial.Web.Models
{
    [NotMapped]
    public class BudgetVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required, and must be unique.")]
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public string User { get; set; }
    }
}