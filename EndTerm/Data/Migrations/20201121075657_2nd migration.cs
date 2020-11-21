using Microsoft.EntityFrameworkCore.Migrations;

namespace EndTerm.Data.Migrations
{
    public partial class _2ndmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Addresses_AddressId",
                table: "Advertisements");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_AddressId",
                table: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Advertisements",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Advertisements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OblastId",
                table: "Advertisements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Advertisements",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favourites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oblasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oblasts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavouritesItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdvertisementId = table.Column<int>(type: "INTEGER", nullable: false),
                    FavouritesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouritesItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouritesItems_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouritesItems_Favourites_FavouritesId",
                        column: x => x.FavouritesId,
                        principalTable: "Favourites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    OblastId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Oblasts_OblastId",
                        column: x => x.OblastId,
                        principalTable: "Oblasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CityId",
                table: "Advertisements",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_OblastId",
                table: "Advertisements",
                column: "OblastId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_UserId1",
                table: "Advertisements",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_OblastId",
                table: "Cities",
                column: "OblastId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_UserId",
                table: "Favourites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouritesItems_AdvertisementId",
                table: "FavouritesItems",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouritesItems_FavouritesId",
                table: "FavouritesItems",
                column: "FavouritesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId1",
                table: "Advertisements",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Cities_CityId",
                table: "Advertisements",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Oblasts_OblastId",
                table: "Advertisements",
                column: "OblastId",
                principalTable: "Oblasts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_AspNetUsers_UserId1",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Cities_CityId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Oblasts_OblastId",
                table: "Advertisements");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "FavouritesItems");

            migrationBuilder.DropTable(
                name: "Oblasts");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_CityId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_OblastId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_UserId1",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "OblastId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Advertisements",
                newName: "AddressId");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Oblast = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_AddressId",
                table: "Advertisements",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Addresses_AddressId",
                table: "Advertisements",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
