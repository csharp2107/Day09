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
                    FirstName = "John", LastName = "Smith", SocialNumber="111"
                });
                db.Students.Add(new Student()
                {
                    FirstName = "Elvis",
                    LastName = "Presley",
                    SocialNumber = "222"
                });
                db.Students.Add(new Student()
                {
                    FirstName = "Max",
                    LastName = "Payne",
                    SocialNumber = "333"
                });
                try
                {
                    db.SaveChanges();
                    Console.WriteLine("Save done...");
                    
                } catch (Exception exc)
                {
                    foreach (var item in db.GetValidationErrors())
                    {
                        foreach (var err in item.ValidationErrors)
                        {
                            Console.WriteLine($"{err.PropertyName} - {err.ErrorMessage}"); 
                        }
                    } 
                }

                Console.ReadKey();
            }
        }
    }
}
