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
            var userPosts = user.Posts;
            return View(userPosts);

        }

    }
}
