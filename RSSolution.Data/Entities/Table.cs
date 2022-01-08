using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSSolution.Data.Entities
{
    public class Table 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Cart> Carts { get; set; }
    }
}