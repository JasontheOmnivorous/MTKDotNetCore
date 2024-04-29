using MTKDotNetCore.ConsoleApp;
using MTKDotNetCore.ConsoleApp.AdoDotNetExamples;
using MTKDotNetCore.ConsoleApp.DapperExamples;
using MTKDotNetCore.ConsoleApp.EFCoreExamples;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

//AdoDotNetExample adoDotNetExamples = new AdoDotNetExample();
//adoDotNetExamples.Read();
//adoDotNetExamples.Create("title", "author", "content");
//adoDotNetExamples.Update(11, "updated title", "updated author", "updated content");
//adoDotNetExamples.Delete(12);
//adoDotNetExamples.Edit(1);

//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.Run();

Console.ReadKey();