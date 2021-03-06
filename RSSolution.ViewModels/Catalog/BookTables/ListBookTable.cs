using RSSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.ViewModels.Catalog.BookTables
{
    public class ListBookTable
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime BookDate { get; set; }
        public int CountAdults { get; set; }
        public int CountChilds { get; set; }
        public string Note { get; set; }
        public BookTableStatus Status { get; set; }
    }
}
