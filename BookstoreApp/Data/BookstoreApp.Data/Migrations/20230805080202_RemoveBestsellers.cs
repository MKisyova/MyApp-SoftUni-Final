using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreApp.Data.Migrations
{
    public partial class RemoveBestsellers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bestsellers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bestsellers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalesCount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestsellers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bestsellers_IsDeleted",
                table: "Bestsellers",
                column: "IsDeleted");
        }
    }
}
