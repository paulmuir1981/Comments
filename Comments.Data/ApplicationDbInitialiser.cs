using Comments.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace Comments.Data
{
    public static class ApplicationDbInitialiser
    {
        private const string _User = "paul@example.com";
        private const string _Password = "Secret123$";
        public static void Initialise(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            var user = userManager.FindByNameAsync(_User).Result;
            if (user == null)
            {
                user = new IdentityUser { UserName = _User, Email = _User };
                var result = userManager.CreateAsync(user, _Password).Result;
                if (!result.Succeeded)
                {
                    throw new Exception("Unable to create dummy user");
                }

                DateTime now = DateTime.UtcNow;
                DateTime yearAgo = now.AddYears(-1);
                DateTime monthAgo = now.AddMonths(-1);
                DateTime dayAgo = now.AddDays(-1);

                CommentHeader[] commentHeaders = new CommentHeader[]
                {
                    new CommentHeader
                    {
                        User = user,
                        CommentRevisions = new CommentRevision[]
                        {
                            new CommentRevision
                            {
                                CreatedDate = yearAgo,
                                Text = "This is my first comment"
                            }
                        }
                    },
                    new CommentHeader
                    {
                        User = user,
                        CommentRevisions = new CommentRevision[]
                        {
                            new CommentRevision
                            {
                                CreatedDate = monthAgo,
                                Text = "This is my second comment"
                            },
                            new CommentRevision
                            {
                                CreatedDate = dayAgo,
                                Text = "This is my second comment which has been edited"
                            },
                            new CommentRevision
                            {
                                CreatedDate = now,
                                Text = "This is my second comment which has been edited twice"
                            }
                        }
                    }
                };

                context.CommentHeaders.AddRange(commentHeaders);
                context.SaveChanges();
            }
        }
    }
}
