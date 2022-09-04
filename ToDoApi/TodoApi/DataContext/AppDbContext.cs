﻿using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }

        
    }
}