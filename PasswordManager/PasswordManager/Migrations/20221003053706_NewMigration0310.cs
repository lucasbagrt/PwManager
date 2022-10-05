using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasswordManager.Migrations
{
    public partial class NewMigration0310 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "passwords",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_passwords_user_id",
                table: "passwords",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_passwords_users_user_id",
                table: "passwords",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_passwords_users_user_id",
                table: "passwords");

            migrationBuilder.DropIndex(
                name: "IX_passwords_user_id",
                table: "passwords");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "passwords");
        }
    }
}
