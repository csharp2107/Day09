using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6MySqlDbTest
{
    
    [Table("StudentTable")]
    public class Student
    {
        [Key]
        [Column("Id", Order =0)]
        public int Id { get; set; }

        [Column("FName", Order = 2)]
        public string FirstName { get; set; }

        [Index(IsUnique = false)]
        [Column("LName", Order = 1)]
        public string LastName { get; set; }


        [Index(IsUnique =true)]
        [MaxLength(10, ErrorMessage ="Social number too long")]
        [MinLength(2, ErrorMessage = "Social number too short")]
        public string SocialNumber { get; set; }

        [NotMapped]
        public int Age { get; set; }

        private int _age;
        public int Test1 { get; }

    }

    [Table("TeacherTable")]
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
