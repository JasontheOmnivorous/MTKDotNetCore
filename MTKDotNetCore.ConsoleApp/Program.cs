using MTKDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

//AdoDotNetExamples adoDotNetExamples = new AdoDotNetExamples();
//adoDotNetExamples.Read();
//adoDotNetExamples.Create("title", "author", "content");
//adoDotNetExamples.Update(11, "updated title", "updated author", "updated content");
//adoDotNetExamples.Delete(12);
//adoDotNetExamples.Edit(1);

DapperExample dapperExample = new DapperExample();
dapperExample.Run();

Console.ReadKey();