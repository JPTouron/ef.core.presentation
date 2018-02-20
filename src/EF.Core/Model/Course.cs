using System.Collections.Generic;

namespace EF.Core.Model
{

    public partial class Course
    {
        public int CourseId { get; set; }

        /// <summary>
        /// as ef core does not support many to many relationships as ef 6 did
        /// we have to specify the intermediate Relational Entity/Table
        /// and so we refer to it rether than having a list of Students right int this class
        /// <para>
        /// old way to link a many to many
        /// public List{Student} Students { get; set; }
        /// </para>
        /// </summary>
        public List<CourseStudent> CourseStudents { get; set; }

        public int Credits { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public string Title { get; set; }
    }

    public partial class Course
    {
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Display => $"{CourseId} - {Title}";

        //public IEnumerable<Student> GetStudentsForCourse(SchoolContext c)
        //{
        //    return c.Students.SelectMany(x => x.CourseStudent).Where(x => x.CourseId == this.CourseId).Select(x => x.Student);
        //}
    }
}