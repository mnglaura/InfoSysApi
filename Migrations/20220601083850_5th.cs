using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace infosysapi.Migrations
{
    public partial class _5th : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    semester = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    studentid = table.Column<string>(type: "text", nullable: true),
                    courseid = table.Column<string>(type: "text", nullable: true),
                    grade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grades", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "homeworks",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    courseid = table.Column<string>(type: "text", nullable: false),
                    studentid = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    submissiondate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_homeworks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "professors",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    firstname = table.Column<string>(type: "text", nullable: true),
                    lastname = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "profteachings",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    profid = table.Column<string>(type: "text", nullable: true),
                    courseid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profteachings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "studenrollments",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    studentid = table.Column<string>(type: "text", nullable: true),
                    courseid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studenrollments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    firstname = table.Column<string>(type: "text", nullable: true),
                    lastname = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "grades");

            migrationBuilder.DropTable(
                name: "homeworks");

            migrationBuilder.DropTable(
                name: "professors");

            migrationBuilder.DropTable(
                name: "profteachings");

            migrationBuilder.DropTable(
                name: "studenrollments");

            migrationBuilder.DropTable(
                name: "students");
        }
    }
}
