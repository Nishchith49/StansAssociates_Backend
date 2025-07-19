using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StansAssociates_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedRouteIdinStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "route",
                table: "student");

            migrationBuilder.DropColumn(
                name: "route_cost",
                table: "student");

            migrationBuilder.AddColumn<long>(
                name: "routeId",
                table: "student",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_student_routeId",
                table: "student",
                column: "routeId");

            migrationBuilder.AddForeignKey(
                name: "FK_student_route_routeId",
                table: "student",
                column: "routeId",
                principalTable: "route",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_student_route_routeId",
                table: "student");

            migrationBuilder.DropIndex(
                name: "IX_student_routeId",
                table: "student");

            migrationBuilder.DropColumn(
                name: "routeId",
                table: "student");

            migrationBuilder.AddColumn<string>(
                name: "route",
                table: "student",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "route_cost",
                table: "student",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
