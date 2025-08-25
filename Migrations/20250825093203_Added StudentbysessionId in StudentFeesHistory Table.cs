using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StansAssociates_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedStudentbysessionIdinStudentFeesHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentbysession_session_session_id",
                table: "studentbysession");

            migrationBuilder.DropForeignKey(
                name: "FK_studentbysession_student_student_id",
                table: "studentbysession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_studentbysession",
                table: "studentbysession");

            migrationBuilder.RenameTable(
                name: "studentbysession",
                newName: "student_by_session");

            migrationBuilder.RenameIndex(
                name: "IX_studentbysession_student_id",
                table: "student_by_session",
                newName: "IX_student_by_session_student_id");

            migrationBuilder.RenameIndex(
                name: "IX_studentbysession_session_id",
                table: "student_by_session",
                newName: "IX_student_by_session_session_id");

            migrationBuilder.AddColumn<long>(
                name: "student_by_session_id",
                table: "student_fees_history",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_student_by_session",
                table: "student_by_session",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_student_fees_history_student_by_session_id",
                table: "student_fees_history",
                column: "student_by_session_id");

            migrationBuilder.AddForeignKey(
                name: "FK_student_by_session_session_session_id",
                table: "student_by_session",
                column: "session_id",
                principalTable: "session",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_student_by_session_student_student_id",
                table: "student_by_session",
                column: "student_id",
                principalTable: "student",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_student_fees_history_student_by_session_student_by_session_id",
                table: "student_fees_history",
                column: "student_by_session_id",
                principalTable: "student_by_session",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_student_by_session_session_session_id",
                table: "student_by_session");

            migrationBuilder.DropForeignKey(
                name: "FK_student_by_session_student_student_id",
                table: "student_by_session");

            migrationBuilder.DropForeignKey(
                name: "FK_student_fees_history_student_by_session_student_by_session_id",
                table: "student_fees_history");

            migrationBuilder.DropIndex(
                name: "IX_student_fees_history_student_by_session_id",
                table: "student_fees_history");

            migrationBuilder.DropPrimaryKey(
                name: "PK_student_by_session",
                table: "student_by_session");

            migrationBuilder.DropColumn(
                name: "student_by_session_id",
                table: "student_fees_history");

            migrationBuilder.RenameTable(
                name: "student_by_session",
                newName: "studentbysession");

            migrationBuilder.RenameIndex(
                name: "IX_student_by_session_student_id",
                table: "studentbysession",
                newName: "IX_studentbysession_student_id");

            migrationBuilder.RenameIndex(
                name: "IX_student_by_session_session_id",
                table: "studentbysession",
                newName: "IX_studentbysession_session_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_studentbysession",
                table: "studentbysession",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_studentbysession_session_session_id",
                table: "studentbysession",
                column: "session_id",
                principalTable: "session",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_studentbysession_student_student_id",
                table: "studentbysession",
                column: "student_id",
                principalTable: "student",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
