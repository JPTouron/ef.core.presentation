using System;
using System.Collections.Generic;

namespace EF.Core.Model
{
    public class Department
    {
        public int? Administrator { get; set; }

        public decimal Budget { get; set; }

        public List<Course> Courses { get; set; }

        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }
    }
}