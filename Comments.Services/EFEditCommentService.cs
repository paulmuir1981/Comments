using System;
using System.Linq;
using System.Threading.Tasks;
using Comments.Data;
using Comments.Data.Entities;
using Comments.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Comments.Services
{
    public class EFEditCommentService : IEditCommentService
    {
        private readonly ApplicationDbContext _context;

        public EFEditCommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Task for getting a comment to edit
        /// </summary>
        /// <param name="userId">The current user's unique id</param>
        /// <param name="commentHeaderId">The comment header's id</param>
        /// <returns>Task for getting a comment to edit</returns>
        public async Task<CommentEditViewModel> ViewCommentAsync(string userId, int? commentHeaderId = null)
        {
            var model = null as CommentEditViewModel;
            if (commentHeaderId.HasValue)
            {
                model = await
                    _context
                    .CommentHeaders
                    .AsNoTracking()
                    .Where(x => x.Id == commentHeaderId && x.UserId == userId) // user can only edit their own comments
                    .Select(x => new CommentEditViewModel(x.Id, x.CommentRevisions.OrderByDescending(y => y.CreatedDate).FirstOrDefault().Text))
                    .FirstOrDefaultAsync();
            }

            if (model == null)
            {
                model = new CommentEditViewModel();
            }

            return model;
        }

        /// <summary>
        /// Task for adding a comment
        /// </summary>
        /// <param name="model">The comment to add</param>
        /// <returns>Task for adding a comment</returns>
        public async Task AddCommentAsync(CommentAddModel model)
        {
            CommentHeader header = new CommentHeader
            {
                UserId = model.UserId,
                CommentRevisions = new CommentRevision[]
                {
                    new CommentRevision
                    {
                        CreatedDate = DateTime.UtcNow,
                        Text = model.Text
                    }
                }
            };
            _context.CommentHeaders.Add(header);
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Task for adding a revision to a comment header
        /// </summary>
        /// <param name="model">the revision to add</param>
        /// <returns>Task for adding a revision to a comment header</returns>
        public async Task AddRevisionAsync(RevisionAddModel model)
        {
            CommentRevision revision = new CommentRevision
            {
                CommentHeaderId = model.CommentHeaderId,
                CreatedDate = DateTime.UtcNow,
                Text = model.Text
            };
            _context.CommentRevisions.Add(revision);
            await _context.SaveChangesAsync();
        }
    }
}
