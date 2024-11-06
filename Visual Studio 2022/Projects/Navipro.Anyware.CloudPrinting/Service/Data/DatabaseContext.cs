using Navipro.Anyware.CloudPrinting.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Navipro.Anyware.CloudPrinting.Service.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<PrintQueueEntry> PrintQueueEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrintQueueEntry>().ToTable("PrintQueueEntry");
        }
    }
}