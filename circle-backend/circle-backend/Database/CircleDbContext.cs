using System;
using circle_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace circle_backend
{
    public class CircleDbContext : DbContext
    {
		public DbSet<Session> Sessions { get; set; }
		public DbSet<User> Users { get; set; }
  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql("Host=localhost;Database=circledb;Username=postgres;Password=circle@123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Session>().HasIndex(s => s.Code).IsUnique();
            modelBuilder.Entity<Session>().Property(s => s.Code).HasMaxLength(4);
        }
    }
}
