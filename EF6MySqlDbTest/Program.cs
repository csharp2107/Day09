using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6MySqlDbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MySqlDbContext())
            {
                db.Students.Add(new Student()
                {
                    FirstName = "John", LastName = "Smith"
                });
                db.SaveChanges();
                Console.WriteLine("Save done...");
                Console.ReadKey();
            }
        }
    }
}
