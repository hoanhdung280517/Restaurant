using Microsoft.EntityFrameworkCore;
using RSSolution.Data.EF;
using RSSolution.Data.Entities;
using RSSolution.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.System.Comments
{
    public class CommentService: ICommentService
    {
        private readonly RSDbContext _context;
        public CommentService(RSDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(CommentCreateRequest request)
        {
            if (request == null)
                throw new Exception("Comment cannot be null");
            var comment = new Comment()
            {
                UserName = request.UserName,
                Comments = request.Comments,
                mealId = request.mealId,
                DateCreate = DateTime.Now,
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment.Id;
        }
        public async Task<List<CommentVm>> GetByMealId(int id)
        {
            var comment = await _context.Comments.Select(x => new ListComment()
            {
                Id = x.Id,
                UserName = x.UserName,
                Comments = x.Comments,
                mealId = x.mealId,
                DateCreate = x.DateCreate
            }).ToListAsync();
            var listcomment = new List<CommentVm>();
            foreach(var item in comment)
            {
                if( item.mealId == id) 
                {
                    var comments = new CommentVm()
                    {
                        Id = item.Id,
                        UserName = item.UserName,
                        Comments = item.Comments,
                        mealId = item.mealId,
                        DateCreate = item.DateCreate

                    };
                    listcomment.Add(comments);
                }
                
            } 

            return listcomment;
        }
    }
}
