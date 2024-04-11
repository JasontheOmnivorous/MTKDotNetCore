using MTKDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

AdoDotNetExamples adoDotNetExamples = new AdoDotNetExamples();
adoDotNetExamples.Read();
adoDotNetExamples.Create("title", "author", "content");

Console.ReadKey();