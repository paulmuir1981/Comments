namespace Comments.ViewModels
{
    /// <summary>
    /// Model for adding a comment revision
    /// </summary>
    public class RevisionAddModel
    {
        public int CommentHeaderId { get; set; }
        public string Text { get; set; }
    }
}
