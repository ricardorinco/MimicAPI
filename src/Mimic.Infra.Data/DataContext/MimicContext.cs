using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mimic.Domain.Models;
using System;

namespace Mimic.Infra.Data.DataContext
{
    public class MimicContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DbSet<Word> Words { get; set; }

        public MimicContext() { }
        public MimicContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlite(DataSource());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        private string DataSource()
        {
            var path = configuration.GetSection("SQLite").GetSection("Path").Value;
            var database = configuration.GetSection("SQLite").GetSection("Database").Value;

            return $"Data Source={path}\\{database}";
        }
    }
}
