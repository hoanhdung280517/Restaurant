using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Comments { get; set; }
        public DateTime DateCreate { get; set; }
        public int mealId { get; set; }
        public Meal meal { get; set; }
    }
}
