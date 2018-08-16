using System;

namespace NormiesRe.Post
{
    public interface INewPostService
    {
        void AddPostByFormModel(NewPostFormModel newPostFormModel);
    }

    public class NewPostService : INewPostService
    {
        private readonly IPostRepository postRepository;

        public NewPostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public void AddPostByFormModel(NewPostFormModel newPostFormModel)
        {
            var post = new Models.Post();
            post.Content = newPostFormModel.Content;
            post.Title = newPostFormModel.Title;
            post.ReleaseDate = DateTime.Now;
            post.Country = newPostFormModel.Country;
            
            postRepository.SavePost(post);
        }
    }
}
