using System.Collections.Immutable;
using System.Linq;
using NormiesRe.Models;

namespace NormiesRe.Post
{
    public interface IPostListService
    {
        PostListViewModel GetNewestPosts();
    }

    public class PostListService : IPostListService
    {
        private readonly IPostRepository postRepository;

        public PostListService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public PostListViewModel GetNewestPosts()
        {
            var postEntities = postRepository.GetLatestPosts(25);
            var viewModels = postEntities.Select(p =>
                new PostViewModel(
                    id: p.ID, 
                    content: p.Content, 
                    country: p.Country,
                    releaseDate: p.ReleaseDate, 
                    title: p.Title)
            ).ToImmutableList();
            
            return new PostListViewModel(viewModels);
        }
    }
}
