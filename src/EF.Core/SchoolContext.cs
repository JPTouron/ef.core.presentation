﻿using EF.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EF.Core
{
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

        public DbSet<Course> Courses { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = School; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

            optionsBuilder.UseSqlServer(connString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //define the pk on the relational table as a combination of the fk related here
            modelBuilder.Entity<CourseStudent>().HasKey(x => new { x.CourseId, x.StudentId }).HasName("Id");

            base.OnModelCreating(modelBuilder);
        }
    }
}