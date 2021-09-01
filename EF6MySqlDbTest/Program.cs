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
                
                try
                {
                    db.Students.Add(new Student()
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        SocialNumber = "111",
                        CardNumber = "", Age = 15
                    });

                    db.Students.Add(new Student()
                    {
                        FirstName = "Elvis",
                        LastName = "Presley",
                        SocialNumber = "2226456362535353645647647567567",
                        CardNumber = ""
                    });
                    db.Students.Add(new Student()
                    {
                        FirstName = "Max",
                        LastName = "Payne",
                        SocialNumber = "333"                        
                    });
                    db.Students.Add(new Student()
                    {
                        FirstName = "Ana",
                        LastName = "Gray",
                        SocialNumber = "334",
                        CardNumber = "",
                        TimeStamp = DateTime.Now
                    });

                    db.SaveChanges();
                    Console.WriteLine("Save done...");

                    db.Students.Local.Clear(); // 
                    // !!! NOT CORRECT - db.Students.RemoveRange(db.Students.ToList()); 
                    db.Students.Add(new Student()
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        SocialNumber = "111111",
                        CardNumber = "",
                        Age = 21
                    });
                    db.SaveChanges();

                    // update one entity of Student
                    var student = db.Students.Find(1);
                    student.CardNumber = "12345";
                    db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    Console.WriteLine("Update done...");

                } catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
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
