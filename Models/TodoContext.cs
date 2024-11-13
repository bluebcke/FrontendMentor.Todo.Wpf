using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMentor.Todo.Wpf.Models
{

    // EF Core "hello world" for reference:
    // https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli

    class TodoContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public string DbPath { get; }

        public TodoContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "todo.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
