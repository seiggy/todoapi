using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoApi.Core.Model;

namespace ToDoApi.InMemory.EntityFramework
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ToDoList>().HasMany<ToDoItem>(e => e.ToDoItems).WithOne(e => e.ToDoList).OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
    }
}
