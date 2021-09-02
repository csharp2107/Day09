using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace EFDBFirstDemo
{
    public class FileLogger
    {
        public static void Log(string msg)
        {
            File.AppendAllText("c:/tmp/ef.log", msg);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //LoggerData();
            //TransactionsSupport();
            //CustomValidation();
            //LazyEagerExplicitLoading();
            CacheData();
            Console.ReadKey();
        }

        private static void CacheData()
        {
            using (var ctx = new MyDbContext())
            {
                ctx.Database.Log = Console.WriteLine;

                Console.WriteLine("========== CACHE1 ============");
                var stud = ctx.Student.FromCache().Where(s => s.StudentID == 2).FirstOrDefault();
                Console.WriteLine(stud.StudentName);

                Console.WriteLine("========== CACHE2 ============");
                stud = ctx.Student.FromCache().Where(s => s.StudentID == 2).FirstOrDefault();
                Console.WriteLine(stud.StudentName);

            }
        }

        private static void LazyEagerExplicitLoading()
        {
            using (var ctx = new MyDbContext())
            {
                ctx.Database.Log = Console.WriteLine;

                // LAZY LOADING
                var student1 = ctx.Student.Where(s => s.StudentID == 1).FirstOrDefault<Student>();
                if (student1!=null)
                {
                    if (student1.StudentAddress!=null)
                        Console.WriteLine(student1.StudentAddress.City);
                }

                // EAGER LOADING
                Console.WriteLine("=================================");
                student1 = ctx.Student.Include("StudentAddress")
                    .Where(s => s.StudentID == 1).FirstOrDefault<Student>();
                if (student1 != null)
                {
                    Console.WriteLine(student1.StudentAddress.City);
                }

                Console.WriteLine("=================================");
                // EXPLICIT LOADING
                student1 = ctx.Student.Where(s => s.StudentID == 1).FirstOrDefault<Student>();
                if (student1 != null)
                {
                    ctx.Entry(student1).Reference(s => s.StudentAddress).Load(); // loads StudentAddress entity
                    Console.WriteLine(student1.StudentAddress.City);
                }
            }
        }

        private static void CustomValidation()
        {
            using (var ctx = new MyDbContext())
            {
                ctx.Student.Add(new Student() { StudentName = "K" });
                try
                {
                    ctx.SaveChanges();
                } catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }

        static void LoggerData()
        {
            using (var ctx = new MyDbContext())
            {
                //ctx.Database.Log = Console.WriteLine;
                ctx.Database.Log = msg => FileLogger.Log(msg);

                var students = ctx.Student.ToList();
                foreach (var item in students)
                {
                    Console.WriteLine($"{item.StudentID} - {item.StudentName}");
                }
            }
        }


        static void TransactionsSupport()
        {
            using (var ctx = new MyDbContext())
            {
                using (DbContextTransaction transaction = ctx.Database.BeginTransaction() )
                {
                    try
                    {
                        ctx.Student.Add(new Student()
                        {
                            StudentName = "John Smith"
                        });
                        ctx.SaveChanges();

                        throw new Exception("dummy exception");

                        ctx.Student.Add(new Student()
                        {
                            StudentName = "John Smith"
                        });
                        ctx.SaveChanges();

                        transaction.Commit();
                    } catch (Exception exc)
                    {
                        transaction.Rollback();
                        Console.WriteLine(exc.Message);
                    }

                    var students = ctx.Student.ToList();
                    foreach (var item in students)
                    {
                        Console.WriteLine($"{item.StudentID} - {item.StudentName}");
                    }
                }
            }
        }
    }
}
