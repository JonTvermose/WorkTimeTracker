using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTimeTracker.Models;

namespace WorkTimeTracker
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());
        }

        public DbSet<Day> Days { get; set; }
    }
}
