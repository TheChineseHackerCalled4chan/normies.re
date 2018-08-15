namespace NormiesRe.Post
{
    public interface IPostFindService
    {
        PostViewModel FindPostById(int id);
    }

    public class PostFindService : IPostFindService
    {
        private readonly IPostRepository postRepository;

        public PostFindService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public PostViewModel FindPostById(int id)
        {
            var entity = postRepository.FindPostById(id);
            if (entity == null)
            {
                return null;
            }
            
            var viewModel = new PostViewModel(
                id: entity.ID, 
                title: entity.Title, 
                content: entity.Content, 
                releaseDate: entity.ReleaseDate);
            
            return viewModel;
        }
    }
}
