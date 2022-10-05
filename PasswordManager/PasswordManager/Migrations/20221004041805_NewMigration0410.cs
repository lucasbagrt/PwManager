using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PasswordManager.Migrations
{
    public partial class NewMigration0410 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "passwords");

            migrationBuilder.AddColumn<int>(
                name: "application_id",
                table: "passwords",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "applications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    icon = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applications", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_passwords_application_id",
                table: "passwords",
                column: "application_id");

            migrationBuilder.AddForeignKey(
                name: "FK_passwords_applications_application_id",
                table: "passwords",
                column: "application_id",
                principalTable: "applications",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_passwords_applications_application_id",
                table: "passwords");

            migrationBuilder.DropTable(
                name: "applications");

            migrationBuilder.DropIndex(
                name: "IX_passwords_application_id",
                table: "passwords");

            migrationBuilder.DropColumn(
                name: "application_id",
                table: "passwords");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "passwords",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
