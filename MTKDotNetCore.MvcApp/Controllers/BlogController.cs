using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.MvcApp.Database;
using MTKDotNetCore.MvcApp.Models;

namespace MTKDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;

        public BlogController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await _db.Blogs.ToListAsync();
            return View(lst);
        }

        // use BlogCreate for uniqueness of method name but when we use it, it's name will be just Create
        // same concept with renaming stuffs
        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> BlogCreate(BlogModel blog)
        {
            Console.WriteLine($"BlogModel: {blog}");
            await _db.Blogs.AddAsync(blog);
            await _db.SaveChangesAsync();

            // return to Blog page after saving
            return Redirect("/Blog");
        }
    }
}
