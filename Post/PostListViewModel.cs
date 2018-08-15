using System;
using System.Collections.Generic;

namespace NormiesRe.Post
{
    public class PostListViewModel
    {
        public readonly IReadOnlyCollection<PostViewModel> Posts;

        public PostListViewModel(IReadOnlyCollection<PostViewModel> posts)
        {
            Posts = posts;
        }
    }

    public class PostViewModel
    {
        public readonly int ID;
        public readonly string Title;
        public readonly string Content;
        public readonly DateTime ReleaseDate;
        
        public IReadOnlyCollection<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();

        public PostViewModel(int id, string title, string content, DateTime releaseDate)
        {
            ID = id;
            Title = title;
            Content = content;
            ReleaseDate = releaseDate;
        }
    }

    public class CommentViewModel
    {
        public readonly DateTime ReleaseDate;
        public readonly string Comment;

        public CommentViewModel(DateTime releaseDate, string comment)
        {
            ReleaseDate = releaseDate;
            Comment = comment;
        }
    }
}
