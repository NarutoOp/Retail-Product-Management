using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VendorService.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryCharge = table.Column<double>(type: "float", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    HandInStocks = table.Column<int>(type: "int", nullable: false),
                    ReplinshmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorStock_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorStock_VendorId",
                table: "VendorStock",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorStock");

            migrationBuilder.DropTable(
                name: "Vendor");
        }
    }
}
