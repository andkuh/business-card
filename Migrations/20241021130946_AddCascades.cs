using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessCard.Migrations
{
    public partial class AddCascades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Duty_Assignments_AssignmentId",
                table: "Duty");

            migrationBuilder.AddForeignKey(
                name: "FK_Duty_Assignments_AssignmentId",
                table: "Duty",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Duty_Assignments_AssignmentId",
                table: "Duty");

            migrationBuilder.AddForeignKey(
                name: "FK_Duty_Assignments_AssignmentId",
                table: "Duty",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id");
        }
    }
}
