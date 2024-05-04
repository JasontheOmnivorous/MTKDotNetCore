using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.ConsoleApp.Services;
using MTKDotNetCore.RestApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace MTKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        // Read
        [HttpGet]
        public IActionResult GetBlogs ()
        {
            string query = "select * from tbl_blog";
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            List<BlogModel> lst = db.Query<BlogModel>(query).ToList();
            return Ok(lst);
        }

        // Edit
        [HttpGet("{id}")]
        public IActionResult GetBlog (int id)
        {
            var item = FindById(id);

            if (item is null)
            {
                return NotFound("No data found.");
            }

            return Ok(item);
        }

        // Create
        [HttpPost]
        public IActionResult CreateBlog (BlogModel blog)
        {
            string query = @"
            INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Saving successful!" : "Saving failed.";

            return Ok(message);
        }

        // Update
        [HttpPut("{id}")]
        public IActionResult UpdateBlog (int id, BlogModel blog)
        {
            var item = FindById(id);

            if (item is null)
            {
                return NotFound("No data found.");
            }

            blog.BlogId = id;
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Update successful!" : "Update failed.";

            return Ok(message);
        }

        // Update
        [HttpPatch("{id}")]
        public IActionResult PatchBlog (int id, BlogModel blog)
        {
            var item = FindById(id);

            if (item is null)
            {
                return NotFound("No data found.");
            }

            string conditions = string.Empty;

            // if certain field is added to update, add their query part to a string
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent";
            }

            if (conditions.Length == 0)
            {
                return NotFound("No data to update.");
            }

            // remove the comma and space before WHERE clause to correct the syntax
            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.BlogId = id;

            // nest the condition string inside the query
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Update successful!" : "Update failed.";

            return Ok(message);
        }

        // Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog (int id)
        {
            var item = FindById(id);

            if (item is null)
            {
                return NotFound("No data found.");
            }

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, new BlogModel {  BlogId = id });
            string message = result > 0 ? "Delete successful!" : "Delete failed";

            return Ok(message);
        }

        private BlogModel? FindById (int id)
        {
            string query = "select * from tbl_blog where blogid = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
            return item;
        }
    }
}
