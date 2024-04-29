using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.ConsoleApp.Dtos;
using MTKDotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleApp.EFCoreExamples
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<BlogDto> Blogs { get; set; }
    }
}
