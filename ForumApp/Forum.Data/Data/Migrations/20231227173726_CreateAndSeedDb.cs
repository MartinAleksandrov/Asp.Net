using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Data.Data.Migrations
{
    public partial class CreateAndSeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[] { new Guid("0f47f782-9ebe-4ad4-9b85-9dac52eba54d"), "My Third Post using ASP.NET", "ThirdPost" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[] { new Guid("d42a1e41-c04f-478b-9219-f372879e0d69"), "My First Post using ASP.NET", "FisrtPost" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[] { new Guid("e01b7449-a618-4089-a978-bdf7bde5d66f"), "My Second Post using ASP.NET", "SecondPost" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
