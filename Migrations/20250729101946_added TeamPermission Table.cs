using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StansAssociates_Backend.Migrations
{
    /// <inheritdoc />
    public partial class addedTeamPermissionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "schoolId",
                table: "user",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "module",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "team_permissions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    team_id = table.Column<long>(type: "bigint", nullable: false),
                    module_id = table.Column<long>(type: "bigint", nullable: false),
                    can_view = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    can_add = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    can_edit = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    can_delete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_permissions", x => x.id);
                    table.ForeignKey(
                        name: "FK_team_permissions_module_module_id",
                        column: x => x.module_id,
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_team_permissions_user_team_id",
                        column: x => x.team_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                column: "schoolId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_user_schoolId",
                table: "user",
                column: "schoolId");

            migrationBuilder.CreateIndex(
                name: "IX_team_permissions_module_id",
                table: "team_permissions",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_team_permissions_team_id",
                table: "team_permissions",
                column: "team_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_user_schoolId",
                table: "user",
                column: "schoolId",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_user_schoolId",
                table: "user");

            migrationBuilder.DropTable(
                name: "team_permissions");

            migrationBuilder.DropTable(
                name: "module");

            migrationBuilder.DropIndex(
                name: "IX_user_schoolId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "schoolId",
                table: "user");
        }
    }
}
