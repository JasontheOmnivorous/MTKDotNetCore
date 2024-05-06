using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.ConsoleApp.Services;
using MTKDotNetCore.RestApi.Models;
using System.Data;
using System.Data.SqlClient;
using MTKDotNetCore.Shared;

namespace MTKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs ()
        {
            string query = "select * from tbl_blog";

            var lst = _adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog (int id)
        {
            string query = "select * from tbl_blog where BlogId = @BlogId";

            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));

            if (item is null)
            {
                return NotFound("No data found.");
            }

            return Ok(item);
        }

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

            var result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
            );

            string message = result > 0 ? "Saving successful!" : "Saving failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog (int id, BlogModel blog)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Update successful!" : "Update failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog (int id, BlogModel blog)
        {
            string findQuery = "select * from Tbl_Blog where BlogId = @BlogId";

            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(findQuery, new AdoDotNetParameter("@BlogId", id));

            if (item is null)
            {
                return NotFound("No data found.");
            }

            string conditions = String.Empty;

            if (!String.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }

            if (!String.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }

            if (!String.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return NotFound("No data to update.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.BlogId = id;

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId = @BlogId";

            var result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogId", id),
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
            );

            string message = result > 0 ? "Patch Update Successful!" : "Patch Update Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog (int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";

            var result = _adoDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", id));

            string message = result > 0 ? "Delete successful!" : "Delete failed.";
            return Ok(message);
        }
    }
}
