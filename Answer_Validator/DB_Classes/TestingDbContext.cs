using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Answer_Validator.DB_Classes
{
    /// <summary>
    /// This class is for creating database or connection to it if its already exists
    /// </summary>
    public class TestingDbContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Result> Results { get; set; }

        public TestingDbContext()
        {
            if (!this.Database.CanConnect()) {

                this.Database.EnsureCreated();
                Console.WriteLine("------ Database has been created successfully ------");
            }

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // To test the app path needs to be changed
            optionsBuilder.UseSqlServer(@"Data Source=192.168.104.57;Initial Catalog=TestingDb;User ID=sa;Password=165415aaBB;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;")
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
                //.LogTo(Console.WriteLine, LogLevel.Information);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .Property(p => p.Id)
                .HasColumnName("UserId");

            modelBuilder.Entity<Result>().HasKey(p => p.ResultId);

            modelBuilder.Entity<AppUser>().Property(p => p.UserName).IsRequired();

            modelBuilder.Entity<Result>()
                .HasOne(k => k.AppUser)
                .WithMany(c => c.Results)
                .HasForeignKey(fk => fk.UserId);

        }
    }
}
