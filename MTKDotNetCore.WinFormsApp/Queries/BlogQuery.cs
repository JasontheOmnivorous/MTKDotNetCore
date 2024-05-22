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


}
