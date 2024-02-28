using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace House_Renting.Data.Migrations
{
    public partial class addingIsActiveColumnInHouses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("2deb92e4-bf9e-44a6-8df0-4e131bde3c0a"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("7e6058bf-5a56-4eb0-b10b-92e40e95920f"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("b50f7e7a-670d-499f-88dd-cdf957fda8cc"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Houses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 28, 19, 17, 46, 104, DateTimeKind.Local).AddTicks(1677),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 1, 26, 18, 6, 8, 902, DateTimeKind.Utc).AddTicks(6914));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Houses",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("44848244-b335-4de8-ba5e-0dc5812082ca"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("45163289-e0e4-4881-b946-e65e624532e1"), 2, "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("4c0a2169-ac11-434c-ae1a-e07d676cd12b"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("45163289-e0e4-4881-b946-e65e624532e1"), 2, "It has the best comfort you will ever ask for. With two bedrooms,it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a & o = &hp = 1", 1200.00m, null, "Family House Comfort" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("676342ab-f488-4896-be34-3e026f52b3a4"), "North London, UK (near the border)", new Guid("45163289-e0e4-4881-b946-e65e624532e1"), 3, "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://www.luxury-architecture.net/wpcontent/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg", 2100.00m, new Guid("5f06cc60-858d-4149-1dc8-08dc1dcba994"), "Big House Marina" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("44848244-b335-4de8-ba5e-0dc5812082ca"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("4c0a2169-ac11-434c-ae1a-e07d676cd12b"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("676342ab-f488-4896-be34-3e026f52b3a4"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Houses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Houses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 26, 18, 6, 8, 902, DateTimeKind.Utc).AddTicks(6914),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 28, 19, 17, 46, 104, DateTimeKind.Local).AddTicks(1677));

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("2deb92e4-bf9e-44a6-8df0-4e131bde3c0a"), "Boyana Neighbourhood, Sofia, Bulgaria", new Guid("45163289-e0e4-4881-b946-e65e624532e1"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This luxurious house is everything you will need. It is just excellent.", "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg", 2000.00m, null, "Grand House" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("7e6058bf-5a56-4eb0-b10b-92e40e95920f"), "North London, UK (near the border)", new Guid("45163289-e0e4-4881-b946-e65e624532e1"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A big house for your whole family. Don't miss to buy a house with three bedrooms.", "https://www.luxury-architecture.net/wpcontent/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg", 2100.00m, new Guid("5f06cc60-858d-4149-1dc8-08dc1dcba994"), "Big House Marina" });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "CreatedOn", "Description", "ImageUrl", "PricePerMonth", "RenterId", "Title" },
                values: new object[] { new Guid("b50f7e7a-670d-499f-88dd-cdf957fda8cc"), "Near the Sea Garden in Burgas, Bulgaria", new Guid("45163289-e0e4-4881-b946-e65e624532e1"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "It has the best comfort you will ever ask for. With two bedrooms,it is great for your family.", "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a & o = &hp = 1", 1200.00m, null, "Family House Comfort" });
        }
    }
}
