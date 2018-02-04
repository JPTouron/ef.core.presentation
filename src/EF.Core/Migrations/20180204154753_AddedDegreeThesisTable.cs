using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EF.Core.Migrations
{
    public partial class AddedDegreeThesisTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Students",
                newName: "AverageGrades");

            migrationBuilder.CreateTable(
                name: "DegreeThesis",
                columns: table => new
                {
                    DegreeThesisId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompletionDate = table.Column<DateTime>(nullable: false),
                    Mentor = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<int>(nullable: false), // non-nullable column!!
                    ThesisSubject = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DegreeThesis", x => x.DegreeThesisId);
                    table.ForeignKey(// now my DegreeThesis table has a FK to StudentId on Student table, this is what we actually want
                        name: "FK_DegreeThesis_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade); // cascade on delete, woot!
                });

            migrationBuilder.CreateIndex( // finally we got an index for the DegreeThesis.StudentId column
                name: "IX_DegreeThesis_StudentId",
                table: "DegreeThesis",
                column: "StudentId",
                unique: true);// ensures that we never duplicate StudentId within DegreeThesis table, therefore, ensuring a 1..1 relationship
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DegreeThesis");

            migrationBuilder.RenameColumn(
                name: "AverageGrades",
                table: "Students",
                newName: "Grade");
        }
    }
}
