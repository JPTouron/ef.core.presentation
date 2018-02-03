using System.Collections.Generic;

namespace EF.Core.Model
{
    public class Course
    {
        public int CourseId { get; set; }

        public int Credits { get; set; }

        public int DepartmentId { get; set; }

        public string Title { get; set; }

        //old way to link a many to many
        //public List<Student> Students { get; set; }

        public List<CourseStudent> CourseStudents { get; set; }
    }
}