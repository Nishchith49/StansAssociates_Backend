using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StansAssociates_Backend.Migrations
{
    /// <inheritdoc />
    public partial class addedschoolrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "name" },
                values: new object[] { 4L, "School" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValue: 4L);
        }
    }
}
