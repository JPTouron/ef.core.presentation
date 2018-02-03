using System.Collections.Generic;

namespace EF.Core.Model
{
    public class Student
    {
        //old way to link a many to many
        //public List<Course> Course { get; set; }

        public List<CourseStudent> CourseStudent { get; set; }

        /// <summary>
        /// pk
        /// </summary>
        public int EnrollmentId { get; set; }

        public string FirstName { get; set; }

        public decimal? Grade { get; set; }

        public string LastName { get; set; }

        public int StudentId { get; set; }
    }
}