using RSSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.ViewModels.Catalog.BookTables
{
    public class EditBookTableRequest
    {
        public int Id { get; set; }
        public BookTableStatus Status { get; set; }
    }
}
