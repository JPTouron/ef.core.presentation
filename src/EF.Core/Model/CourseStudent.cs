namespace EF.Core.Model
{
    public class CourseStudent
    {
        public Course Course { get; set; }

        public int CourseId { get; set; }

        public Student Student { get; set; }

        public int StudentId { get; set; }
    }
}