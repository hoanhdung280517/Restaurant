using RSSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Data.Entities
{
    public class BookTable
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime BookDate { get; set; }
        public int CountAdults { get; set; }
        public int CountChilds { get; set; }
        public AppUser User { get; set; }
        public string Note { get; set; }
        public BookTableStatus Status { get; set; }
    }
}
