namespace EF.Core.Model
{
    public class CourseStudent
    {
        public Course Course { get; set; }

        public int CourseId { get; set; }
        [System.ComponentModel.DataAnnotations.ScaffoldColumn(false)]
        public int Id { get; set; }

        public Student Student { get; set; }

        public int StudentId { get; set; }
    }
}