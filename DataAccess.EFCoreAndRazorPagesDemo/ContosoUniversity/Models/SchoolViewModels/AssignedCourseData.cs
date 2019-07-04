using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models.SchoolViewModels
{
    [NotMapped]
    public class AssignedCourseData
    {
        public int CourseID { get; set; }

        public string Title { get; set; }

        public bool Assigned { get; set; }

    }
}
