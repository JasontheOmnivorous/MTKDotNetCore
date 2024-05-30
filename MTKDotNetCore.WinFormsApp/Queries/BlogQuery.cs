namespace MTKDotNetCore.WinFormsApp.Queries;

public class BlogQuery
{
    public static string BlogCreate { get; } = @"
            INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

    // this is faster than * because when we use *, sql have to check how many tables or columns are there in the DB first
    // this just directly query specified columns, so it's faster
    public static string BlogList { get; } = @"SELECT [BlogId]
  ,[BlogTitle]
  ,[BlogAuthor]
  ,[BlogContent]
 FROM [dbo].[Tbl_Blog]";

    public static string BlogEdit { get; } = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";

    public static string BlogUpdate { get; } = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";
}
