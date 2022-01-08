using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RSSolution.Data.Migrations
{
    public partial class user13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "OrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "DiscountPercent",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "8258873d-5ec6-4357-9227-1ce453198d95");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cc32fed3-6078-41fe-9d3b-d23cb7a81a3b", "AQAAAAEAACcQAAAAEAwmJ4wF2CW/WhkOH3hlz+uxbuuLojVRjpe7QAXs2jvJ1ZNs55idqB9j4MLv20etmw==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 6, 1, 4, 18, 616, DateTimeKind.Local).AddTicks(2535));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "DiscountPercent",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "36daa5e7-7244-4071-a220-6ecdddaabc3c");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "97770ddd-a04a-4d75-8d39-20b8c3bd44b6", "AQAAAAEAACcQAAAAEGxd1A/57IFlAQ8LuJztau07627V4kMIoizipMdCDbpOlUf9F4z0WROoYX+5Y3Vdog==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 6, 0, 1, 22, 508, DateTimeKind.Local).AddTicks(6006));
        }
    }
}
