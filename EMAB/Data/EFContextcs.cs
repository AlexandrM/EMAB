using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EMAB.Data
{
    public class EFContext : DbContext
    {
        public EFContext()
            : base("ConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class DBInit<TContext> : IDatabaseInitializer<TContext> where TContext : System.Data.Entity.DbContext
    {
        public DBInit()
        {
        }

        public void InitializeDatabase(TContext context)
        {
            var configuration = new DbMigrationsConfiguration<TContext>();
            configuration.AutomaticMigrationDataLossAllowed = true;
            configuration.AutomaticMigrationsEnabled = true;
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

    }
}