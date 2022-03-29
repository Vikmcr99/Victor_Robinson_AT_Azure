using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class friendDbContext : DbContext
    {
        public friendDbContext()
        {

        }
        
        public friendDbContext(DbContextOptions<friendDbContext> options) : base(options) {}

        public DbSet<Friend> Friend { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=vikdatabase;Trusted_Connection=True;MultipleActiveResultSets=true");

        }

        
    }
}

