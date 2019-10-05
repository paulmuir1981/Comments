using Comments.ViewModels.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Comments.ViewModels
{
    /// <summary>
    /// View Model for creating/editing a comment
    /// </summary>
    public class CommentEditViewModel
    {
        public CommentEditViewModel() { }
        public CommentEditViewModel(int? id = null, string text = null)
        {
            Id = id;
            OriginalText = text;
            NewText = text;
        }
        /// <summary>
        /// If Id has a value then this is for an existing comment, else its a new comment
        /// </summary>
        public int? Id { get; set; }
        public string OriginalText { get; set; }

        [Display(Name = "New Text")]
        [Required]
        [StringMustNotEqual(nameof(OriginalText))]
        public string NewText { get; set; }

        /// <summary>
        /// Helper for displaying in the edit page 
        /// </summary>
        public virtual string _EditMessage { get { return string.Format("{0} Comment", Id.HasValue ? "Edit" : "Add"); } }
    }
}
