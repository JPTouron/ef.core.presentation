using System;

namespace EF.Core.Model
{
    public class DegreeThesis
    {
        public DateTime CompletionDate { get; set; }

        public int DegreeThesisId { get; set; }

        public string Mentor { get; set; }

        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// this, along with the Student navigation prop are the requirements on this end of the relationship to determine a 1..0.1 relationship between Student and DegreeThesis
        /// </summary>
        public int StudentId { get; set; }

        public Student Student { get; set; }

        public string ThesisSubject { get; set; }
    }
}