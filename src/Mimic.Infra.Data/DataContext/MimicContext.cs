using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mimic.Domain.Models;

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
            var connectionString = configuration.GetConnectionString("SQLite");

            dbContextOptionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
