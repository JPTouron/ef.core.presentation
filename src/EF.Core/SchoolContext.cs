using EF.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EF.Core
{
    /// <summary>
    /// this interface implementation enables the using of migrations right out of the box 
    /// the samy way that we used to do with EF6
    /// refereces:
    /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
    /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/1x-2x-upgrade
    /// https://codingblast.com/entityframework-core-idesigntimedbcontextfactory/
    /// </summary>
    public class DbContextInitilizer : IDesignTimeDbContextFactory<SchoolContext>
    {
        public SchoolContext CreateDbContext(string[] args)
        {
            return new SchoolContext();
        }
    }

    public class SchoolContext : DbContext
    {
        public SchoolContext()
        {
        }

        /// <summary>
        /// enables the passing of options from the web project on startup
        /// </summary>
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {

        }



        public DbSet<Course> Courses { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<DegreeThesis> DegreeThesis { get; set; }

        public DbSet<CourseStudent> CourseStudents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // alternative way to setup db connection string
            // this is required to be uncommented if you use controller scaffolding or migration running

            var connString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = School; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            optionsBuilder.UseSqlServer(connString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //define the pk on the relational table as a combination of the fk related here
            modelBuilder.Entity<CourseStudent>().HasKey(x => new { x.CourseId, x.StudentId });

            //old way of setting up 1..0.1 relationship between two clases, where DegreeThesis may or may not exist
            //modelBuilder.Entity<Student>().HasOptional(x=>x.DegreeThesis).WithRequired(x=>x.Student)

            base.OnModelCreating(modelBuilder);
        }
    }
}