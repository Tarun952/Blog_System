using Blog_System.Models.ViewModels;
using Blog_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog_System.Controllers
{
    public class PostsController : Controller
    {
        private readonly BlogSystemContext _blogSystemContext;
        private readonly UserManager<User> userManager;

        public PostsController(BlogSystemContext blogSystemContext, UserManager<User> userManager)
        {

            _blogSystemContext = blogSystemContext;
            this.userManager = userManager;
        }
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(PostAddRequest postAddRequest)
        {
            var user = await userManager.GetUserAsync(User);
            var userPosts = user.Posts;
            var post = new Post()
            {
                Title = postAddRequest.Title,
                Content = postAddRequest.Content,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = user.Id
            };

            userPosts.Add(post);

            var userModel = new User()
            {
                Posts=userPosts,

            };

            await _blogSystemContext.AddAsync(userModel);

            await _blogSystemContext.AddAsync(post);
            await _blogSystemContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogs = await _blogSystemContext.Posts.ToListAsync();
            return View(blogs);
        }

        //[Authorize]
        //public async Task<ActionResult> Edit(int id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _blogSystemContext.Posts.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user);

        //}

        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> Edit(int? id, Post post)
        //{
        //    var user = await userManager.GetUserAsync(User);
        //    var userId = await _blogSystemContext.Posts.FindAsync(id);
        //    var UserPostId = userId.UserId;
        //    if (userId == null)
        //    {
        //        return NotFound();
        //    }
        //    if(user == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (UserPostId != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var blogPost = await _blogSystemContext.Posts.FindAsync(id);
        //    if (blogPost == null)
        //    {
        //        return NotFound();
        //    }
        //    blogPost.Title = post.Title;
        //    blogPost.Content = post.Content;
        //    blogPost.CreatedAt = DateTime.Now;
        //    blogPost.UpdatedAt = DateTime.Now;

        //    await _blogSystemContext.SaveChangesAsync();


        //    // pass data to view
        //    return RedirectToAction("List");
        //}

        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    // tell repository to delte
        //    var userr = await userManager.GetUserAsync(User);
        //    if (userr == null)
        //    {
        //        return BadRequest();
        //    }
        //    var userId = await _blogSystemContext.Posts.FindAsync(id);
        //    if (userId == null)
        //    {
        //        return NotFound();
        //    }
        //    var UserPostId = userId.UserId;
            
        //    if (UserPostId != userr.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var user = await _blogSystemContext.Posts.FindAsync(id);
        //    if (user != null)
        //    {
        //        _blogSystemContext.Posts.Remove(user);
        //    }

        //    await _blogSystemContext.SaveChangesAsync();
        //    return RedirectToAction("List");

        //}

    }
}
