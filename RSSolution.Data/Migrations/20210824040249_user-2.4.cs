using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RSSolution.Data.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BookTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f83980c8-b750-4960-afad-e749b526da22");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a2c098f0-37a0-426f-bc8f-0ee88b37cbee", "AQAAAAEAACcQAAAAEIfS7W1fgFHGIITY2gI5OagOxTCGH8nV4zrN4OW1zRMjiPNUD7SDlbHrbZSSGvKtGw==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 24, 11, 2, 48, 164, DateTimeKind.Local).AddTicks(993));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "BookTables");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "d79197a3-a5e8-4064-8cfb-c62a282c6fab");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f200c1ce-2cbc-4dd3-ac15-3f37798d3e45", "AQAAAAEAACcQAAAAEIazEMb0ld3iIBrLoru1NSHTRVhz2nM8FPDWKeysbB7QkPlCeHeDsJdRuhTkNi2P5g==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 23, 17, 34, 6, 439, DateTimeKind.Local).AddTicks(310));
        }
    }
}
