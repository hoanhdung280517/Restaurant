using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.ViewModels.Catalog.BookTables
{
    public class BookTableRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int CountAdults { get; set; }
        public int CountChilds { get; set; }
        public DateTime BookDate { get; set; }
        public string Note { get; set; }
    }
}
