using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StansAssociates_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedSchoolIdwhereneed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_user_schoolId",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "schoolId",
                table: "user",
                newName: "school_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_schoolId",
                table: "user",
                newName: "IX_user_school_id");

            migrationBuilder.AddColumn<long>(
                name: "school_id",
                table: "student",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "school_id",
                table: "route",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_student_school_id",
                table: "student",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_route_school_id",
                table: "route",
                column: "school_id");

            migrationBuilder.AddForeignKey(
                name: "FK_route_user_school_id",
                table: "route",
                column: "school_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_student_user_school_id",
                table: "student",
                column: "school_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_user_school_id",
                table: "user",
                column: "school_id",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_route_user_school_id",
                table: "route");

            migrationBuilder.DropForeignKey(
                name: "FK_student_user_school_id",
                table: "student");

            migrationBuilder.DropForeignKey(
                name: "FK_user_user_school_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_student_school_id",
                table: "student");

            migrationBuilder.DropIndex(
                name: "IX_route_school_id",
                table: "route");

            migrationBuilder.DropColumn(
                name: "school_id",
                table: "student");

            migrationBuilder.DropColumn(
                name: "school_id",
                table: "route");

            migrationBuilder.RenameColumn(
                name: "school_id",
                table: "user",
                newName: "schoolId");

            migrationBuilder.RenameIndex(
                name: "IX_user_school_id",
                table: "user",
                newName: "IX_user_schoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_user_schoolId",
                table: "user",
                column: "schoolId",
                principalTable: "user",
                principalColumn: "id");
        }
    }
}
