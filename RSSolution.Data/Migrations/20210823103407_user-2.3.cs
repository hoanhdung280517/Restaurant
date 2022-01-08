using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RSSolution.Data.Migrations
{
    public partial class user23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountAdults",
                table: "BookTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountChilds",
                table: "BookTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountAdults",
                table: "BookTables");

            migrationBuilder.DropColumn(
                name: "CountChilds",
                table: "BookTables");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "afa295de-9c4f-4de6-ae42-1e6a17895f9b");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8b4909b7-582c-4b02-aeff-8247a69af473", "AQAAAAEAACcQAAAAEA+u+7yfqnh3uK2x2YpkzycBxRWodeBkH71bvGlDrD01DI9Pzv3SEUh+S/FnA1ytPQ==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 22, 17, 56, 24, 642, DateTimeKind.Local).AddTicks(2276));
        }
    }
}
