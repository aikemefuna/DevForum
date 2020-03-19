using DevelopersForum.Models;
using DevelopersForum.Models.Interfaces;
using DevelopersForum.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopersForum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);
            var replies = BuildPostReplies(post.Replies);

            var model = new PostIndexModel
            {
                Id = post.PostId,
                Title = post.Title,
                AuthorId = post.ApplicationUsers.Id,
                AuthorName = post.ApplicationUsers.UserName,
                AuthorImageUrl = post.ApplicationUsers.ProfileImageUrl,
                AuthorRating = post.ApplicationUsers.Rating,
                Created = post.Created,
                PostContent = post.Content,
                Replies = replies
            };

            return View(model);
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(reply => new PostReplyModel
            {
                Id = reply.PostReplyId,
                AuthorName = reply.ApplicationUsers.UserName,
                AuthorId = reply.ApplicationUsers.Id,
                AuthorImageUrl = reply.ApplicationUsers.ProfileImageUrl,
                AuthorRating = reply.ApplicationUsers.Rating,
                Created = reply.Created,
                ReplyContent = reply.Content
            });
        }
    }
}
