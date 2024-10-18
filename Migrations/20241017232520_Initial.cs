using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessCard.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    YearsOld = table.Column<int>(type: "INTEGER", nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Summary = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: false),
                    Specialization = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Image_ContentType = table.Column<string>(type: "TEXT", nullable: false),
                    Image_Bytes = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationStep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    YearStarted = table.Column<int>(type: "INTEGER", nullable: false),
                    YearFinished = table.Column<int>(type: "INTEGER", nullable: false),
                    Institution = table.Column<string>(type: "TEXT", nullable: false),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationStep_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployerId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employments_Employers_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employments_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hobby",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobby", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hobby_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EmploymentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: false),
                    Summary = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    Link_Address = table.Column<string>(type: "TEXT", nullable: true),
                    Link_Caption = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Employments_EmploymentId",
                        column: x => x.EmploymentId,
                        principalTable: "Employments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmploymentId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobTitles_Employments_EmploymentId",
                        column: x => x.EmploymentId,
                        principalTable: "Employments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentTechnology",
                columns: table => new
                {
                    AssignmentsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnologiesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentTechnology", x => new { x.AssignmentsId, x.TechnologiesId });
                    table.ForeignKey(
                        name: "FK_AssignmentTechnology_Assignments_AssignmentsId",
                        column: x => x.AssignmentsId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentTechnology_Technologies_TechnologiesId",
                        column: x => x.TechnologiesId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Duty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: false),
                    AssignmentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Duty_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_EmploymentId",
                table: "Assignments",
                column: "EmploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentTechnology_TechnologiesId",
                table: "AssignmentTechnology",
                column: "TechnologiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Duty_AssignmentId",
                table: "Duty",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationStep_PersonId",
                table: "EducationStep",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_EmployerId",
                table: "Employments",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_PersonId",
                table: "Employments",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Hobby_PersonId",
                table: "Hobby",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_EmploymentId",
                table: "JobTitles",
                column: "EmploymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentTechnology");

            migrationBuilder.DropTable(
                name: "Duty");

            migrationBuilder.DropTable(
                name: "EducationStep");

            migrationBuilder.DropTable(
                name: "Hobby");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Employments");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
