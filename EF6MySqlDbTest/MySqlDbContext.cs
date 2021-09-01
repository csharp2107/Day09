using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6MySqlDbTest
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class MySqlDbContext : DbContext
    {
        public MySqlDbContext() : base("MySqlDemoDb")
        {
            Database.SetInitializer<MySqlDbContext>(new DropCreateDatabaseAlways<MySqlDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // force name for the entities
            //modelBuilder.Entity<Student>().ToTable("TableStudents");
            //modelBuilder.Entity<Teacher>().ToTable("TableTeachers");

            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public override int SaveChanges()
        {
            ChangeTracker
                .Entries()
                .Where(e => e.Entity is Student );
            return base.SaveChanges();
        }

    }
}
