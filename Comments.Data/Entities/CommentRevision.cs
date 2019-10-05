using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comments.Data.Entities
{
    public class CommentRevision
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(CommentHeader))]
        public int CommentHeaderId { get; set; }

        public DateTime CreatedDate { get; set; }
        public string Text { get; set; }
        public virtual CommentHeader CommentHeader { get; set; }
    }
}
