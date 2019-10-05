using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comments.Data;
using Comments.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Comments.Services
{
    public class EFViewCommentsService : IViewCommentsService
    {
        private readonly ApplicationDbContext _context;

        public EFViewCommentsService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of all comments
        /// </summary>
        /// <returns>A list of all comments</returns>
        public async Task<List<CommentViewModel>> ViewCommentsAsync()
        {
            return await
                _context
                .CommentHeaders
                .AsNoTracking()
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    CreatedDate = x.CommentRevisions.OrderBy(y => y.CreatedDate).FirstOrDefault().CreatedDate,
                    UpdatedDate = x.CommentRevisions.OrderByDescending(y => y.CreatedDate).FirstOrDefault().CreatedDate,
                    Text = x.CommentRevisions.OrderByDescending(y => y.CreatedDate).FirstOrDefault().Text, //last revision has the comment text
                    UserId = x.UserId,
                    UserName = x.User.UserName
                })
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        /// <summary>
        /// Gets a list of all revisions for a comment header
        /// </summary>
        /// <param name="commentHeaderId">Id of the comment header</param>
        /// <returns>a list of all revisions for a comment header</returns>
        public async Task<List<RevisionViewModel>> ViewCommentRevisionsAsync(int commentHeaderId)
        {
            return await
                _context
                .CommentRevisions
                .AsNoTracking()
                .Where(x => x.CommentHeaderId == commentHeaderId)
                .Select(x => new RevisionViewModel
                {
                    CreatedDate = x.CreatedDate,
                    Text = x.Text
                })
                .OrderByDescending(x => x.CreatedDate)
                .Skip(1) // can already see top 1 so skipping
                .ToListAsync();
        }
    }
}
