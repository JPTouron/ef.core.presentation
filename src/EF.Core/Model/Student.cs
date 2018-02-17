using System.Collections.Generic;

namespace EF.Core.Model
{
    public class Student
    {
        //old way to link a many to many
        //public List<Course> Course { get; set; }

        public decimal? AverageGrades { get; set; }
        public List<CourseStudent> CourseStudent { get; set; }

        

        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// pk
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// enough code on this end of the relationship to determine a 1..0.1 relationship between Student and DegreeThesis
        /// </summary>
        public DegreeThesis Thesis { get; set; }
    }
}