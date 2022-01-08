using RSSolution.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.APIHelppers
{
    public interface ICommentApiClient
    {
        Task<List<CommentVm>> GetByMealId(int mealid);
        Task<bool> Create(CommentCreateRequest request);
    }
}
