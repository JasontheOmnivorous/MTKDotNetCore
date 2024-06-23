﻿using Microsoft.AspNetCore.Mvc;
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
            var lst = await _db.Blogs.OrderByDescending(x => x.BlogId).ToListAsync();
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

        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            var item = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);

            if (item is null) return Redirect("/Blog");

            return View("BlogEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(int id, BlogModel blog)
        {
            var item = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);

            if (item is null) return Redirect("/Blog");

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            await _db.SaveChangesAsync();

            return Redirect("/Blog");
        }

        [HttpDelete]
        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            var item = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);

            if (item is null) return Redirect("/Blog");

            _db.Blogs.Remove(item);
            await _db.SaveChangesAsync();

            return Redirect("/Blog");
        }
    }
}
