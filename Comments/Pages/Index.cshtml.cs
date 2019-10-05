using Comments.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using Comments.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Comments.Services;

namespace Comments.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Service for viewing comments and revisions
        /// </summary>
        private readonly IViewCommentsService _service;
        /// <summary>
        /// All comments
        /// </summary>
        public List<CommentViewModel> Comments { get; set; }

        /// <summary>
        /// Temp message for use after successful coment modification
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IViewCommentsService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            Comments = await _service.ViewCommentsAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentHeaderId"></param>
        /// <returns></returns>
        public async Task<PartialViewResult> OnGetRevisionsPartialAsync(int commentHeaderId)
        {
            var revisions = await _service.ViewCommentRevisionsAsync(commentHeaderId);
            return new PartialViewResult
            {
                ViewName = "_RevisionsPartial",
                ViewData = new ViewDataDictionary<List<RevisionViewModel>>(ViewData, revisions)
            };
        }
    }
}