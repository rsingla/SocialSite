using System.Data;
using Microsoft.Data.SqlClient;
using SocialSite.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SocialSite.Data
{
    public class DataContextEF : DbContext
    {

        private IConfiguration _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<UserSalary>? UserSalary { get; set; }
        public virtual DbSet<UserJobInfo>? UserJobInfo { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                 options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<User>().ToTable("Users", "TutorialAppSchema").HasKey(u => u.UserId);
            modelBuilder.Entity<Error>().HasKey(u => u.Message);
            modelBuilder.Entity<UserJobInfo>().ToTable("UserJobInfo", "TutorialAppSchema").HasKey(u => u.UserId);
            modelBuilder.Entity<UserSalary>().ToTable("UserSalary", "TutorialAppSchema").HasKey(u => u.UserId);
        }

    }

}