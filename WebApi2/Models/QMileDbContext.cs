using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebApi2.Models
{
    public class QMileDbContext : DbContext
    {
        public QMileDbContext():base("name=TripDB")
        {
        }

        public DbSet<EmployeeModel> EmployeeModel { get; set; }

        public DbSet<TripModel> Trips { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Make table names singular instead of plural
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}