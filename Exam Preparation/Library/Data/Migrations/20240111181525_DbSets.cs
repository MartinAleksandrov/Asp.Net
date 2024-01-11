using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Data.Migrations
{
    public partial class DbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Category_CategoryId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserBook_AspNetUsers_CollectorId",
                table: "IdentityUserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserBook_Books_BookId",
                table: "IdentityUserBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserBook",
                table: "IdentityUserBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "IdentityUserBook",
                newName: "IdentityUserBooks");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityUserBook_BookId",
                table: "IdentityUserBooks",
                newName: "IX_IdentityUserBooks_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserBooks",
                table: "IdentityUserBooks",
                columns: new[] { "CollectorId", "BookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserBooks_AspNetUsers_CollectorId",
                table: "IdentityUserBooks",
                column: "CollectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserBooks_Books_BookId",
                table: "IdentityUserBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserBooks_AspNetUsers_CollectorId",
                table: "IdentityUserBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserBooks_Books_BookId",
                table: "IdentityUserBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserBooks",
                table: "IdentityUserBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "IdentityUserBooks",
                newName: "IdentityUserBook");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityUserBooks_BookId",
                table: "IdentityUserBook",
                newName: "IX_IdentityUserBook_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserBook",
                table: "IdentityUserBook",
                columns: new[] { "CollectorId", "BookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Category_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserBook_AspNetUsers_CollectorId",
                table: "IdentityUserBook",
                column: "CollectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserBook_Books_BookId",
                table: "IdentityUserBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
