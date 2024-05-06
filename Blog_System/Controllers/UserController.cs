using Blog_System.Models;
using Blog_System.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog_System.Controllers
{
    public class UserController : Controller
    {
        private readonly BlogSystemContext _blogSystemContext;
        private readonly UserManager<User> userManager;

        public UserController(BlogSystemContext blogSystemContext, UserManager<User> userManager)
        {

            _blogSystemContext = blogSystemContext;
            this.userManager = userManager;
        }

        public async Task<IActionResult>List()
        {
            var user = await userManager.GetUserAsync(User);
            if(user == null)
            {
                return NotFound();
            }
            var blogs = await _blogSystemContext.Posts.Where(x=>x.UserId==user.Id).ToListAsync();
            return View(blogs);

        }

        [Authorize]
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _blogSystemContext.Posts.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int? id, Post post)
        {
            var user = await userManager.GetUserAsync(User);
            var Singlepost = await _blogSystemContext.Posts.FindAsync(id);
            var UserPostId = Singlepost.UserId;
            if (Singlepost == null)
            {
                return NotFound();
            }
            if (user == null)
            {
                return BadRequest();
            }
            if (UserPostId != user.Id)
            {
                return BadRequest();
            }

            if (id == null)
            {
                return NotFound();
            }

            Singlepost.Title = post.Title;
            Singlepost.Content = post.Content;
            Singlepost.CreatedAt = post.CreatedAt;
            Singlepost.UpdatedAt = DateTime.Now;

            await _blogSystemContext.SaveChangesAsync();


            // pass data to view
            return RedirectToAction("List");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            // tell repository to delte
            var userr = await userManager.GetUserAsync(User);
            if (userr == null)
            {
                return BadRequest();
            }
            var userId = await _blogSystemContext.Posts.FindAsync(id);
            if (userId == null)
            {
                return NotFound();
            }
            var UserPostId = userId.UserId;

            if (UserPostId != userr.Id)
            {
                return BadRequest();
            }

            var user = await _blogSystemContext.Posts.FindAsync(id);
            if (user != null)
            {
                _blogSystemContext.Posts.Remove(user);
            }

            await _blogSystemContext.SaveChangesAsync();
            return RedirectToAction("List");

        }

    }
}
