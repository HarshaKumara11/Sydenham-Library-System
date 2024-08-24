using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sydenham_Library_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookIssueTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BOOKISSUES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prodid = table.Column<int>(type: "int", nullable: false),
                    Issuedto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Createddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Returndate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOOKISSUES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BOOKISSUES_AVAILABILITY_Status",
                        column: x => x.Status,
                        principalTable: "AVAILABILITY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BOOKISSUES_Products_Prodid",
                        column: x => x.Prodid,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BOOKISSUES_Prodid",
                table: "BOOKISSUES",
                column: "Prodid");

            migrationBuilder.CreateIndex(
                name: "IX_BOOKISSUES_Status",
                table: "BOOKISSUES",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BOOKISSUES");
        }
    }
}
