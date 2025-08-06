using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StansAssociates_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Addedlocationtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "user");

            migrationBuilder.DropColumn(
                name: "country",
                table: "user");

            migrationBuilder.DropColumn(
                name: "state",
                table: "user");

            migrationBuilder.DropColumn(
                name: "city",
                table: "student");

            migrationBuilder.DropColumn(
                name: "country",
                table: "student");

            migrationBuilder.DropColumn(
                name: "state",
                table: "student");

            migrationBuilder.AddColumn<long>(
                name: "city_id",
                table: "user",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "country_id",
                table: "user",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "state_id",
                table: "user",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "city_id",
                table: "student",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "country_id",
                table: "student",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "state_id",
                table: "student",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    iso2 = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    latitude = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    longitude = table.Column<decimal>(type: "decimal(18,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "state",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    state_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    iso2 = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country_id = table.Column<long>(type: "bigint", nullable: false),
                    image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    latitude = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    longitude = table.Column<decimal>(type: "decimal(18,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state", x => x.id);
                    table.ForeignKey(
                        name: "FK_state_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    city_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    state_id = table.Column<long>(type: "bigint", nullable: false),
                    image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    latitude = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    longitude = table.Column<decimal>(type: "decimal(18,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city", x => x.id);
                    table.ForeignKey(
                        name: "FK_city_state_state_id",
                        column: x => x.state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "city_id", "country_id", "state_id" },
                values: new object[] { 1L, 1L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_user_city_id",
                table: "user",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_country_id",
                table: "user",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_state_id",
                table: "user",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_city_id",
                table: "student",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_country_id",
                table: "student",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_state_id",
                table: "student",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_city_state_id",
                table: "city",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_state_country_id",
                table: "state",
                column: "country_id");

            migrationBuilder.AddForeignKey(
                name: "FK_student_city_city_id",
                table: "student",
                column: "city_id",
                principalTable: "city",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_student_country_country_id",
                table: "student",
                column: "country_id",
                principalTable: "country",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_student_state_state_id",
                table: "student",
                column: "state_id",
                principalTable: "state",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_city_city_id",
                table: "user",
                column: "city_id",
                principalTable: "city",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_country_country_id",
                table: "user",
                column: "country_id",
                principalTable: "country",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_state_state_id",
                table: "user",
                column: "state_id",
                principalTable: "state",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_student_city_city_id",
                table: "student");

            migrationBuilder.DropForeignKey(
                name: "FK_student_country_country_id",
                table: "student");

            migrationBuilder.DropForeignKey(
                name: "FK_student_state_state_id",
                table: "student");

            migrationBuilder.DropForeignKey(
                name: "FK_user_city_city_id",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_country_country_id",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_state_state_id",
                table: "user");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "state");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropIndex(
                name: "IX_user_city_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_country_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_state_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_student_city_id",
                table: "student");

            migrationBuilder.DropIndex(
                name: "IX_student_country_id",
                table: "student");

            migrationBuilder.DropIndex(
                name: "IX_student_state_id",
                table: "student");

            migrationBuilder.DropColumn(
                name: "city_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "country_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "state_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "city_id",
                table: "student");

            migrationBuilder.DropColumn(
                name: "country_id",
                table: "student");

            migrationBuilder.DropColumn(
                name: "state_id",
                table: "student");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "user",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "user",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "user",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "student",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "student",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "student",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "city", "country", "state" },
                values: new object[] { "", "", "" });
        }
    }
}
