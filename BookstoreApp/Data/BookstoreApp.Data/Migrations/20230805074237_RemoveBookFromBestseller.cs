using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreApp.Data.Migrations
{
    public partial class RemoveBookFromBestseller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestsellers_Books_BookId",
                table: "Bestsellers");

            migrationBuilder.DropIndex(
                name: "IX_Bestsellers_BookId",
                table: "Bestsellers");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Bestsellers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Bestsellers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bestsellers_BookId",
                table: "Bestsellers",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestsellers_Books_BookId",
                table: "Bestsellers",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
