using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6MySqlDbTest
{
    class MySqlDbContext : DbContext
    {
        public MySqlDbContext() : base("MySqlDemoDb")
        {
            Database.SetInitializer<MySqlDbContext>(new DropCreateDatabaseAlways<MySqlDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
