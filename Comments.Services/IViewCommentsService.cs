using Comments.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comments.Services
{
    /// <summary>
    /// Service for the main page
    /// </summary>
    public interface IViewCommentsService
    {
        /// <summary>
        /// Gets a list of all comments
        /// </summary>
        /// <returns>A list of all comments</returns>
        Task<List<CommentViewModel>> ViewCommentsAsync();

        /// <summary>
        /// Gets a list of all revisions for a comment header
        /// </summary>
        /// <param name="commentHeaderId">Id of the comment header</param>
        /// <returns>a list of all revisions for a comment header</returns>
        Task<List<RevisionViewModel>> ViewCommentRevisionsAsync(int commentHeaderId);
    }
}
