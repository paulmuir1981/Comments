using System;

namespace Comments.ViewModels
{
    /// <summary>
    /// View of a comment revision
    /// </summary>
    public class RevisionViewModel
    {
        public DateTime CreatedDate { get; set; }
        public string Text { get; set; }
    }
}
