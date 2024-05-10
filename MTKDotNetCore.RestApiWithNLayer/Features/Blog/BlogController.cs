using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MTKDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _blBlog;

        public BlogController ()
        {
            _blBlog = new BL_Blog ();
        }

        [HttpGet]
        public List<BlogModel> GetBlogs ()
        {
            var lst = _blBlog.GetBlogs();
            return lst;
        }

        [HttpGet("{id}")]
        public BlogModel GetBlog (int id)
        {
            var item = _blBlog.GetBlog(id);
            return item;
        }

        [HttpPost]
        public string CreateBlog (BlogModel requestModel)
        {
            var result = _blBlog.CreateBlog(requestModel);

            string message = result > 0 ? "Saving successful!" : "Saving failed.";
            return message;
        }

        [HttpPut("{id}")]
        public string UpdateBlog (int id, BlogModel requestModel)
        {
            var result = _blBlog.UpdateBlog(id, requestModel);

            string message = result > 0 ? "Update successful!" : "Update failed.";
            return message;
        }

        [HttpPatch("{id}")]
        public string PatchBlog(int id, BlogModel requestModel)
        {
            var result = _blBlog.PatchBlog(id, requestModel);

            string message = result > 0 ? "Patch successful!" : "Patch failed.";
            return message;
        }

        [HttpDelete("{id}")]
        public string DeleteBlog (int id)
        {
            var result = _blBlog.DeleteBlog(id);

            string message = result > 0 ? "Deleting successful!" : "Deleting failed.";
            return message;
        }
    }
}
