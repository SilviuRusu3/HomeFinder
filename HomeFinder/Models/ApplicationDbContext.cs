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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<LocationAttributes> Attributes { get; set; }
        public DbSet<HomeFeatures> Features { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<AreaAttributes> AreaAttributes { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<GradedFeatures> GradedFeatures { get; set; }
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
            modelBuilder.Entity<User>()
               .HasMany(u => u.Areas)
               .WithOne(a => a.User).
               IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AreaAttributes>()
                .HasKey(al => new { al.LocationAttributesId, al.AreaId });
            modelBuilder.Entity<AreaAttributes>()
                .HasOne(al => al.Area)
                .WithMany(a => a.AreaAttributes)
                .HasForeignKey(al => al.AreaId);
            modelBuilder.Entity<AreaAttributes>()
                .HasOne(la => la.LocationAttributes)
                .WithMany(a => a.AreaAttributes)
                .HasForeignKey(bc => bc.LocationAttributesId);
            modelBuilder.Entity<Area>()
                .HasMany(a => a.Homes)
                .WithOne(h => h.Area).
                IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<GradedFeatures>()
                .HasKey(gf => new { gf.HomeFeaturesId, gf.HomeId});
            modelBuilder.Entity<GradedFeatures>()
                .HasOne(gf => gf.Home)
                .WithMany(h => h.GradedFeaturee)
                .HasForeignKey(gf => gf.HomeId);
            modelBuilder.Entity<GradedFeatures>()
                .HasOne(gf => gf.HomeFeatures)
                .WithMany(hf => hf.GradedFeaturee)
                .HasForeignKey(gf => gf.HomeFeaturesId);
        }
    }
}
