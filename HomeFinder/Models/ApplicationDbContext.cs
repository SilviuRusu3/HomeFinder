using HomeFinder.HelpClass;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        static ApplicationDbContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<HomeType>();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
        }
        public DbSet<LocationAttributes> Attributes { get; set; }
        public DbSet<HomeFeatures> Features { get; set; }
        //public DbSet<Review> Reviews { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=localhost;Database=HomeFinder;Username=postgres;Password=123456",
                o => o.UseNetTopologySuite());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresEnum<HomeType>();
            modelBuilder.Entity<User>()
                .HasMany(u => u.LocationAttributes)
                .WithOne(l => l.User).
                IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasMany(u => u.HomeFeatures)
                .WithOne(f => f.User).
                IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
