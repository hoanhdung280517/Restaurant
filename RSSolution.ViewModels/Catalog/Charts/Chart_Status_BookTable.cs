using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.ViewModels.Catalog.Charts
{
    public class Chart_Status_BookTable
    {
        public string UserName { get; set; }
        public int TotalInProgress { get; set; }
        public int TotalConfirmed { get; set; }
        public int TotalSuccess { get; set; }
        public int TotalCanceled { get; set; }
    }
}
