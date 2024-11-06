using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SmartAdminMvc
{
    public class SystemDatabase : DbContext
    {

        public SystemDatabase() : base("SystemDatabaseConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SystemDatabase, MyConfiguration>());
        }

  

        public DbSet<Environment> Environments { get; set; }
        public DbSet<UserEnvironment> UserEnvironments { get; set; }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Device> Devices { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void checkEnvironmentTableStructure(Environment environment)
        {
            Store.checkEnvironmentTableStructure(this, environment);
            Device.checkEnvironmentTableStructure(this, environment);
        }

    }
}