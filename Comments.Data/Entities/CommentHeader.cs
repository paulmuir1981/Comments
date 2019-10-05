using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Data.Entities
{
    public class CommentHeader
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        public virtual ICollection<CommentRevision> CommentRevisions { get; set; }
    }
}
