#nullable disable
namespace House_Renting.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    public partial class SeedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerMounth",
                table: "Houses",
                newName: "PricePerMonth");

            migrationBuilder.RenameColumn(
                name: "ImgageUrl",
                table: "Houses",
                newName: "ImageUrl");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Cottage" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Single-Family" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Duplex" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("21177870-be87-4b26-8a13-6a6d35026bc0"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("45163289-e0e4-4881-b946-e65e624532e1"), 2, "It has the best comfort you will ever ask for. With two bedrooms,it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a & o = &hp = 1", 1200.00m, null, "Family House Comfort" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("4d282edc-aa49-4708-a481-b88cc6ac3157"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("45163289-e0e4-4881-b946-e65e624532e1"), 2, "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("7a9904f9-1731-4021-866a-a94d28f07872"), "North London, UK (near the border)", new Guid("45163289-e0e4-4881-b946-e65e624532e1"), 3, "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://www.luxury-architecture.net/wpcontent/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg", 2100.00m, new Guid("5f06cc60-858d-4149-1dc8-08dc1dcba994"), "Big House Marina" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("21177870-be87-4b26-8a13-6a6d35026bc0"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("4d282edc-aa49-4708-a481-b88cc6ac3157"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("7a9904f9-1731-4021-866a-a94d28f07872"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "PricePerMonth",
                table: "Houses",
                newName: "PricePerMounth");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Houses",
                newName: "ImgageUrl");
        }
    }
}
