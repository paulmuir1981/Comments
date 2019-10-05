using Comments.ViewModels;
using System.Threading.Tasks;

namespace Comments.Services
{
    public interface IEditCommentService
    {
        /// <summary>
        /// Task for getting a comment to edit
        /// </summary>
        /// <param name="userId">The current user's unique id</param>
        /// <param name="commentHeaderId">The comment header's id</param>
        /// <returns>Task for getting a comment to edit</returns>
        Task<CommentEditViewModel> ViewCommentAsync(string userId, int? commentHeaderId = null);

        /// <summary>
        /// Task for adding a comment
        /// </summary>
        /// <param name="model">The comment to add</param>
        /// <returns>Task for adding a comment</returns>
        Task AddCommentAsync(CommentAddModel model);

        /// <summary>
        /// Task for adding a revision to a comment header
        /// </summary>
        /// <param name="model">the revision to add</param>
        /// <returns>Task for adding a revision to a comment header</returns>
        Task AddRevisionAsync(RevisionAddModel model);
    }
}
