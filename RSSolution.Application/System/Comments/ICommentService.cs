using RSSolution.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.System.Comments
{
    public interface ICommentService
    {
        Task<int> Create(CommentCreateRequest request);
        Task<List<CommentVm>> GetByMealId(int id);
    }
}
