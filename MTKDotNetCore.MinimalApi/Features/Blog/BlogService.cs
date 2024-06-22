using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.MinimalApi.Database;
using MTKDotNetCore.MinimalApi.Models;

namespace MTKDotNetCore.MinimalApi.Features.Blog
{
    public static class BlogService
    {
        public static IEndpointRouteBuilder MapBlogs(this IEndpointRouteBuilder app)
        {
            // method dependency injection
            app.MapGet("api/Blog", async (AppDbContext db) =>
            {
                var lst = await db.Blogs.AsNoTracking().ToListAsync();
                return Results.Ok(lst);
            });

            app.MapPost("api/Blog", async (AppDbContext db, BlogModel blog) =>
            {
                await db.Blogs.AddAsync(blog);
                int result = await db.SaveChangesAsync();

                string message = result > 0 ? "Saving successful!" : "Saving failed.";
                return Results.Ok(message);
            });

            app.MapPut("api/Blog/{id}", async (AppDbContext db, int id, BlogModel blog) =>
            {
                var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);

                if (item is null) return Results.NotFound("No data found with that id.");

                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;

                int result = await db.SaveChangesAsync();

                string message = result > 0 ? "Updating successful!" : "Updating failed.";
                return Results.Ok(message);
            });

            app.MapPatch("/api/Blog/{id}", async (AppDbContext db, int id, BlogModel blog) =>
            {
                var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);

                if (item is null) return Results.NotFound("No data found with that id.");

                if (!string.IsNullOrEmpty(blog.BlogTitle))
                    item.BlogTitle = blog.BlogTitle;

                if (!string.IsNullOrEmpty(blog.BlogAuthor))
                    item.BlogAuthor = blog.BlogAuthor;

                if (!string.IsNullOrEmpty(blog.BlogContent))
                    item.BlogContent = blog.BlogContent;

                int result = await db.SaveChangesAsync();

                string message = result > 0 ? "Update successful!" : "Update failed.";

                return Results.Ok(message);
            });

            app.MapDelete("api/Blog/{id}", async (AppDbContext db, int id) =>
            {
                var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);

                if (item is null) return Results.NotFound("No data found with that id.");

                db.Blogs.Remove(item);

                int result = await db.SaveChangesAsync();

                string message = result > 0 ? "Deleting successful!" : "Deleting failed.";
                return Results.Ok(message);
            });

            return app;
        }
    }
}
