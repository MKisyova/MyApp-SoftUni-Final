using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreApp.Data.Migrations
{
    public partial class ShoppingCartBooksFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookShoppingCarts");

            migrationBuilder.CreateTable(
                name: "ShoppingCartBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartBooks_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartBooks_BookId",
                table: "ShoppingCartBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartBooks_ShoppingCartId",
                table: "ShoppingCartBooks",
                column: "ShoppingCartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartBooks");

            migrationBuilder.CreateTable(
                name: "BookShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookShoppingCarts_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookShoppingCarts_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookShoppingCarts_BookId",
                table: "BookShoppingCarts",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookShoppingCarts_ShoppingCartId",
                table: "BookShoppingCarts",
                column: "ShoppingCartId");
        }
    }
}
