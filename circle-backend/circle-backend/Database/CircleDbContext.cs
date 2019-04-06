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

            optionsBuilder.UseNpgsql("Host=localhost;Database=circledb;Username=my_user;Password=my_pw");
        }
    }
}
