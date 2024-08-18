using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sydenham_Library_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class Prodtbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TITLE = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AUTHOR = table.Column<int>(type: "int", nullable: false),
                    PRODTYPES = table.Column<int>(type: "int", nullable: false),
                    GENRES = table.Column<int>(type: "int", nullable: false),
                    AVAILABILITY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_AUTHOR_AUTHOR",
                        column: x => x.AUTHOR,
                        principalTable: "AUTHOR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_AVAILABILITY_AVAILABILITY",
                        column: x => x.AVAILABILITY,
                        principalTable: "AVAILABILITY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_GENRES_GENRES",
                        column: x => x.GENRES,
                        principalTable: "GENRES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_PRODUCTTYPES_PRODTYPES",
                        column: x => x.PRODTYPES,
                        principalTable: "PRODUCTTYPES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_AUTHOR",
                table: "Products",
                column: "AUTHOR");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AVAILABILITY",
                table: "Products",
                column: "AVAILABILITY");

            migrationBuilder.CreateIndex(
                name: "IX_Products_GENRES",
                table: "Products",
                column: "GENRES");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PRODTYPES",
                table: "Products",
                column: "PRODTYPES");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
