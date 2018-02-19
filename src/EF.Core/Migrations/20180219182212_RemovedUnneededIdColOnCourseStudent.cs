using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EF.Core.Migrations
{
    public partial class RemovedUnneededIdColOnCourseStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "Id",
                table: "CourseStudents");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseStudents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseStudents",
                table: "CourseStudents",
                columns: new[] { "CourseId", "StudentId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseStudents",
                table: "CourseStudents");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CourseStudents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "Id",
                table: "CourseStudents",
                columns: new[] { "CourseId", "StudentId" });
        }
    }
}
