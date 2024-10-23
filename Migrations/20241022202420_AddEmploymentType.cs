using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessCard.Migrations
{
    public partial class AddEmploymentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Employments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Employments");
        }
    }
}
