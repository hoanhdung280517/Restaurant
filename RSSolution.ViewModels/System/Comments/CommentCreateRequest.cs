using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.ViewModels.Comments
{
    public class CommentCreateRequest
    {
        public string UserName { get; set; }
        public string Comments { get; set; }
        public int mealId { get; set; }
    }
}
