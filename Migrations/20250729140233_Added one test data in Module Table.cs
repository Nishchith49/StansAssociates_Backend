using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StansAssociates_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedonetestdatainModuleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "module",
                columns: new[] { "id", "name" },
                values: new object[] { 1L, "Dashboard" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "module",
                keyColumn: "id",
                keyValue: 1L);
        }
    }
}
