using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            TransactionsSupport();
            Console.ReadKey();
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
                }
            }
        }
    }
}
