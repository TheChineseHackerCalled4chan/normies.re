using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NormiesRe.Models;

namespace NormiesRe.Post
{
    public interface IPostRepository
    {
        IReadOnlyCollection<Models.Post> GetLatestPosts(int amount);
        void SavePost(Models.Post post);
        Models.Post FindPostById(int id);
    }

    public class PostRepository : IPostRepository
    {
        private readonly NormiesDbContext dbContext;

        public PostRepository(NormiesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IReadOnlyCollection<Models.Post> GetLatestPosts(int amount)
        {
            var posts = dbContext.Posts
                .OrderByDescending(p => p.ReleaseDate)
                .Take(25)
                .ToImmutableList();

            return posts;
        }

        public void SavePost(Models.Post post)
        {
            dbContext.Add(post);
            dbContext.SaveChanges();
        }

        public Models.Post FindPostById(int id)
        {
            return dbContext.Posts.FirstOrDefault(p => p.ID == id);
        }
    }
}
