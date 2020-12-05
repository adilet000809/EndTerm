using Microsoft.EntityFrameworkCore.Migrations;

namespace EndTerm.Data.Migrations
{
    public partial class Many2Onefixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId1",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_UserId1",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Advertisements");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Advertisements",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_UserId",
                table: "Advertisements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId",
                table: "Advertisements",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_UserId",
                table: "Advertisements");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Advertisements",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Advertisements",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_UserId1",
                table: "Advertisements",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId1",
                table: "Advertisements",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
