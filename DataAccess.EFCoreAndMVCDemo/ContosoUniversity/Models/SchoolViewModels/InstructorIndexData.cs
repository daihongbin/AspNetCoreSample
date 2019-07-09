using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models.SchoolViewModels
{
    [NotMapped]
    public class InstructorIndexData
    {
        public IEnumerable<Instructor> Instructors { get; set; }

        public IEnumerable<Course> Courses { get; set; }

        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
