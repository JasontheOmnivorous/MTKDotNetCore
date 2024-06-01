using MTKDotNetCore.NLayer.DataAccess.Db;
using MTKDotNetCore.NLayer.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.NLayer.DataAccess.Services;

public class DA_Blog
{
    private readonly AppDbContext _context;

    public DA_Blog ()
    {
        _context = new AppDbContext ();
    }

    public List<BlogModel> GetBlogs ()
    {
        var lst = _context.Blogs.ToList ();
        return lst;
    }

    public BlogModel GetBlog (int id)
    {
        var item = SearchItem(id);
        return item;
    }

    public int CreateBlog (BlogModel requestModel)
    {
        _context.Blogs.Add(requestModel);
        int result = _context.SaveChanges();

        return result;
    }

    public int UpdateBlog (int id, BlogModel requestModel)
    {
        var item = SearchItem(id);

        if (item is null) return 0;

        item.BlogTitle = requestModel.BlogTitle;
        item.BlogAuthor = requestModel.BlogAuthor;
        item.BlogContent = requestModel.BlogContent;

        int result = _context.SaveChanges();

        return result;
    }

    public int PatchBlog (int id, BlogModel requestModel)
    {
        var item = SearchItem(id);

        if (item is null) return 0;

        if (!string.IsNullOrEmpty(requestModel.BlogTitle)) item.BlogTitle = requestModel.BlogTitle;

        if (!string.IsNullOrEmpty(requestModel.BlogAuthor)) item.BlogAuthor = requestModel.BlogAuthor;

        if (!string.IsNullOrEmpty(requestModel.BlogContent)) item.BlogContent = requestModel.BlogContent;

        int result = _context.SaveChanges();

        return result;
    }

    public int DeleteBlog (int id)
    {
        var item = SearchItem(id);

        if (item is null) return 0;

        _context.Blogs.Remove(item);

        int result = _context.SaveChanges();

        return result;
    }

    public BlogModel SearchItem (int id)
    {
        var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id)!;

        return item;
    }
}

