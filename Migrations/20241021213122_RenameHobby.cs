using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessCard.Migrations
{
    public partial class RenameHobby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationStep_People_PersonId",
                table: "EducationStep");

            migrationBuilder.DropForeignKey(
                name: "FK_Hobby_People_PersonId",
                table: "Hobby");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hobby",
                table: "Hobby");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationStep",
                table: "EducationStep");

            migrationBuilder.RenameTable(
                name: "Hobby",
                newName: "Hobbies");

            migrationBuilder.RenameTable(
                name: "EducationStep",
                newName: "EducationSteps");

            migrationBuilder.RenameIndex(
                name: "IX_Hobby_PersonId",
                table: "Hobbies",
                newName: "IX_Hobbies_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_EducationStep_PersonId",
                table: "EducationSteps",
                newName: "IX_EducationSteps_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hobbies",
                table: "Hobbies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationSteps",
                table: "EducationSteps",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationSteps_People_PersonId",
                table: "EducationSteps",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobbies_People_PersonId",
                table: "Hobbies",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationSteps_People_PersonId",
                table: "EducationSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_Hobbies_People_PersonId",
                table: "Hobbies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hobbies",
                table: "Hobbies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationSteps",
                table: "EducationSteps");

            migrationBuilder.RenameTable(
                name: "Hobbies",
                newName: "Hobby");

            migrationBuilder.RenameTable(
                name: "EducationSteps",
                newName: "EducationStep");

            migrationBuilder.RenameIndex(
                name: "IX_Hobbies_PersonId",
                table: "Hobby",
                newName: "IX_Hobby_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_EducationSteps_PersonId",
                table: "EducationStep",
                newName: "IX_EducationStep_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hobby",
                table: "Hobby",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationStep",
                table: "EducationStep",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationStep_People_PersonId",
                table: "EducationStep",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobby_People_PersonId",
                table: "Hobby",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id");
        }
    }
}
