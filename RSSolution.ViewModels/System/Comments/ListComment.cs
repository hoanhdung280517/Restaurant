using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.ViewModels.Comments
{
    public class ListComment
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreate { get; set; }
        public string Comments { get; set; }
        public int mealId { get; set; }
    }
}
