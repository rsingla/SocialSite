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

        public DataContextEF(IConfiguration config) {
          _config = config;
        }

       public DbSet<Computer>? Computer { get; set; }
       protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured) 
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                 options => options.EnableRetryOnFailure());
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<Computer>().ToTable("Computer").HasKey(c => c.ComputerId);
        }

    }
    
}