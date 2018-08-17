using System;
using Microsoft.AspNetCore.Mvc;
using NormiesRe.Post;

namespace NormiesRe.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostListService postListService;
        private readonly INewPostService newPostService;
        private readonly IPostFindService postFindService;
        private readonly IPostDeleteService postDeleteService;
        private readonly INewCommentService newCommentService;

        public HomeController(IPostListService postListService, 
            INewPostService newPostService, 
            IPostFindService postFindService, 
            IPostDeleteService postDeleteService,
            INewCommentService newCommentService)
        {
            this.postListService = postListService;
            this.newPostService = newPostService;
            this.postFindService = postFindService;
            this.postDeleteService = postDeleteService;
            this.newCommentService = newCommentService;
        }

        [Route("")]
        public IActionResult Index() => View(postListService.GetNewestPosts());

        [Route("/new")]
        public IActionResult New() => View();

        [Route("/show/{id}")]
        public IActionResult Show(int id)
        {
            var viewModel = postFindService.FindPostById(id);
            if (viewModel == null)
            {
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [Route("/delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (Environment.GetEnvironmentVariable("DELETE_KEY") == Request.Query["key"])
            {
                postDeleteService.DeletePostById(id);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("/addpost")]
        public IActionResult AddPost([Bind("Title,Content")] NewPostFormModel newPostFormModel)
        {
            // fuck off 
            if (string.IsNullOrWhiteSpace(newPostFormModel.Title)
                || string.IsNullOrWhiteSpace(newPostFormModel.Content)
                || Uri.EscapeDataString(newPostFormModel.Title).Contains("%C2%AD")
                || Uri.EscapeDataString(newPostFormModel.Content).Contains("%C2%AD")
                || newPostFormModel.Content.Length > 20000 || newPostFormModel.Title.Length > 200)
            {
                return RedirectToAction("Index");
            }

            newPostFormModel.Country = Request.Headers["CF-IPCountry"].ToString();
            
            newPostService.AddPostByFormModel(newPostFormModel);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Route("/addcomment/{postid}")]
        public IActionResult AddComment(int postid, [Bind("Content")] NewCommentFormModel newPostFormModel)
        {

            if (string.IsNullOrWhiteSpace(newPostFormModel.Content)
                || Uri.EscapeDataString(newPostFormModel.Content).Contains("%C2%AD")
                || newPostFormModel.Content.Length > 20000)
            {
                return RedirectToAction("Index");
            }

            var post = postFindService.FindPostById(postid);
            if (post == null)
            {
                return RedirectToAction("Index");
            }
            
            newCommentService.AddCommentToPost(postid, newPostFormModel);
            return RedirectToAction("Show", new { id = postid });
        }
    }
}
