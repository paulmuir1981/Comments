using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Comments.Data.Entities;
using Comments.Services;
using Comments.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Comments.Pages
{
    /// <summary>
    /// Page for adding / editing comments. Should only be accessed by logged on users (see startup)
    /// </summary>
    public class EditModel : PageModel
    {
        /// <summary>
        /// Service for adding / editing comments
        /// </summary>
        private readonly IEditCommentService _service;

        /// <summary>
        /// Model to be added / edited
        /// </summary>
        [BindProperty]
        public CommentEditViewModel CommentEditModel { get; set; }

        /// <summary>
        /// Temp message for use after successful coment modification
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }
        public EditModel(IEditCommentService service)
        {
            _service = service;
        }

        public async Task OnGetAsync(int? commentHeaderId)
        {
            CommentEditModel = await _service.ViewCommentAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), commentHeaderId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // if model is valid then process
            if(ModelState.IsValid)
            {
                var mode = "edit";

                // if this is a revision to an existing comment then add revision
                if (CommentEditModel.Id.HasValue)
                {
                    RevisionAddModel model = new RevisionAddModel
                    {
                        CommentHeaderId = CommentEditModel.Id.Value,
                        Text = CommentEditModel.NewText
                    };
                    await _service.AddRevisionAsync(model);
                }
                // Else add comment - need current user id
                else
                {
                    mode = "add";
                    CommentAddModel model = new CommentAddModel
                    {
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        Text = CommentEditModel.NewText
                    };
                    await _service.AddCommentAsync(model);
                }

                StatusMessage = string.Format("Comment {0}ed successfully", mode);
                return RedirectToPage("Index");
            }
            // else redisplay page - validation errors will be displayed
            return Page();
        }
    }
}