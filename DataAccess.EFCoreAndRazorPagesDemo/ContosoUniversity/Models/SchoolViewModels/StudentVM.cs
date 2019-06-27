using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models.SchoolViewModels
{
    [NotMapped]
    public class StudentVM
    {
        public int ID { get; set; }

        public string LastName { get; set; }

        public string FirstMidName { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
