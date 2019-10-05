using System;

namespace Comments.ViewModels
{
    /// <summary>
    /// View of a comment
    /// </summary>
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Text { get; set; }
        public virtual bool _IsEdited { get { return CreatedDate != UpdatedDate; } }
    }
}
