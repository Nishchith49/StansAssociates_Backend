using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StansAssociates_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatusNamecolumninEmployeeAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status_name",
                table: "employee_attendance",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status_name",
                table: "employee_attendance");
        }
    }
}
