namespace NormiesRe.Post
{
    public interface IPostDeleteService
    {
        void DeletePostById(int id);
    }

    public class PostDeleteService : IPostDeleteService
    {
        private readonly IPostRepository postRepository;

        public PostDeleteService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public void DeletePostById(int id)
        {
            postRepository.DeletePostById(id);
        }
    }
}
