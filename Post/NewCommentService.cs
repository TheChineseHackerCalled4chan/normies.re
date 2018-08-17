using System;
using NormiesRe.Models;

namespace NormiesRe.Post
{
    public interface INewCommentService
    {
        void AddCommentToPost(int postId, NewCommentFormModel newPostFormModel);
    }

    public class NewCommentService : INewCommentService
    {
        private readonly IPostRepository postRepository;

        public NewCommentService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public void AddCommentToPost(int postId, NewCommentFormModel newPostFormModel)
        {
            var comment = new Comment();
            comment.Content = newPostFormModel.Content;
            comment.Post = postRepository.FindPostById(postId);
            comment.ReleaseDate = DateTime.Now;
            comment.Country = newPostFormModel.Country;
            
            postRepository.SaveComment(comment);
        }
    }
}
