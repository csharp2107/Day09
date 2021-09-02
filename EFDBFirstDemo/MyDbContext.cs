using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace EFDBFirstDemo
{
    class MyDbContext : school_dbEntities
    {

        public MyDbContext() : base()
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, 
            IDictionary<object, object> items)
        {

            if (entityEntry.Entity is Student)
            {
                string value = entityEntry.CurrentValues.GetValue<string>("StudentName");
                if (value==null || value.Trim().Length<2)
                {
                    var list = new List<DbValidationError>();
                    list.Add(new DbValidationError("StudentName", "Incorrect length"));
                    return new DbEntityValidationResult(entityEntry, list);
                }
            }
            return base.ValidateEntity(entityEntry, items);
        }

    }
}
