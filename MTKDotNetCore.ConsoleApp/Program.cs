using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MTKDotNetCore.ConsoleApp;
using MTKDotNetCore.ConsoleApp.AdoDotNetExamples;
using MTKDotNetCore.ConsoleApp.DapperExamples;
using MTKDotNetCore.ConsoleApp.EFCoreExamples;
using MTKDotNetCore.ConsoleApp.Services;
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

//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Run();

var connectionString = ConnectionStrings.sqlConnectionStringBuilder.ConnectionString;
var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

// dependency injection for console app using Microsoft.Extensions.DependencyInjection package
// add AppDbContext in the injection first and only after that, we can add it's depending code
var serviceProvider = new ServiceCollection()
    .AddScoped(n => new AdoDotNetExample(sqlConnectionStringBuilder))
    .AddScoped(n => new DapperExample(sqlConnectionStringBuilder))
    .AddDbContext<AppDbContext>(option =>
    {
        option.UseSqlServer(connectionString);
    })
    .AddScoped<EFCoreExample>()
    .BuildServiceProvider();

//AppDbContext context = serviceProvider.GetRequiredService<AppDbContext>();

// use injected codes
var adoDotNetExample = serviceProvider.GetRequiredService<AdoDotNetExample>();
adoDotNetExample.Read();

var dapperExample = serviceProvider.GetRequiredService<DapperExample>();
dapperExample.Run();

Console.ReadKey();