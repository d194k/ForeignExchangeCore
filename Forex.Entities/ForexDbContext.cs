using Forex.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Forex.Entities
{
    public class ForexDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<ExchangeRateSyncBase> ExchangeRateSyncBase { get; set; }
        public DbSet<ExchangeRate> ExchangeRate { get; set; }

        //public ForexDbContext(IConfiguration config)
        //    : base()
        //{
        //    _config = config;
        //    Database.SetCommandTimeout(300);
        //}

        // Used for migration
        public ForexDbContext()
            : base()
        {
            Database.SetCommandTimeout(300);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_config.GetConnectionString("ForexDbContext"));

            // Used for migration
            optionsBuilder.UseSqlServer("server=.;database=ForexDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
