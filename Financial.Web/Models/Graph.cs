using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Financial.Web.Models
{
    [NotMapped]
    public class Graph
    {
        public string Key { get; set; }
        public decimal Value { get; set; }
    }
}