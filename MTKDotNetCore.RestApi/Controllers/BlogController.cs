using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.RestApi.Database;
using MTKDotNetCore.RestApi.Models;

namespace MTKDotNetCore.RestApi.Controllers
{
    // endpoint will be api/blog
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        //private readonly AppDbContext _context;

        //public BlogController()
        //{
        //    _context = new AppDbContext();
        //}

        private readonly AppDbContext _context;

        public BlogController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpGet]
        public IActionResult Read()
        {
            var lst = _context.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                return NotFound("No data found.");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _context.Blogs.Add(blog);
            int result = _context.SaveChanges();

            string message = result > 0 ? "Saving successful!" : "Saving failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            int result = _context.SaveChanges();

            string message = result > 0 ? "Update successful!" : "Update failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                return NotFound("No data found.");
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle))
                item.BlogTitle = blog.BlogTitle;

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
                item.BlogAuthor = blog.BlogAuthor;

            if (!string.IsNullOrEmpty(blog.BlogContent))
                item.BlogContent = blog.BlogContent;

            int result = _context.SaveChanges();

            string message = result > 0 ? "Update successful!" : "Update failed.";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                return NotFound("No data found.");
            }

            _context.Blogs.Remove(item);

            int result = _context.SaveChanges();

            string message = result > 0 ? "Delete successful!" : "Delete failed.";

            return Ok(message);
        }
    }
}
