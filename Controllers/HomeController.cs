﻿using Microsoft.AspNetCore.Mvc;
using NormiesRe.Post;

namespace NormiesRe.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostListService postListService;
        private readonly INewPostService newPostService;
        private readonly IPostFindService postFindService;

        public HomeController(IPostListService postListService, 
            INewPostService newPostService, 
            IPostFindService postFindService)
        {
            this.postListService = postListService;
            this.newPostService = newPostService;
            this.postFindService = postFindService;
        }
        
        [Route("")]
        public IActionResult Index()
        {
            return View(postListService.GetNewestPosts());
        }

        [Route("/new")]
        public IActionResult New()
        {
            return View();
        }

        [Route("/show/{id}")]
        public IActionResult Show(int id)
        {
            var viewModel = postFindService.FindPostById(id);
            if (viewModel == null)
            {
                return Redirect("/");
            }

            return View(viewModel);
        }

        [HttpPost]
        [Route("/addpost")]
        public IActionResult AddPost([Bind("Title,Content")] NewPostFormModel newPostFormModel)
        {
            newPostService.AddPostByFormModel(newPostFormModel);
            return Redirect("/");
        }
    }
}
