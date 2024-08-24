using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sydenham_Library_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class Reservationtbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RESERVATIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prodid = table.Column<int>(type: "int", nullable: false),
                    Reservedby = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Reservedbyphone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Createddate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESERVATIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESERVATIONS_Products_Prodid",
                        column: x => x.Prodid,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATIONS_Prodid",
                table: "RESERVATIONS",
                column: "Prodid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RESERVATIONS");
        }
    }
}
