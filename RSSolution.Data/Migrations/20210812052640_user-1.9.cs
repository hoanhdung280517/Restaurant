using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RSSolution.Data.Migrations
{
    public partial class user19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "6458b745-1752-44a6-8452-110d0b43ecae");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d95cecac-6b81-4230-ae01-251b23aebebc", "AQAAAAEAACcQAAAAEGILIBbA82RKEskRXLY0AHjfKVXG+WiBuNq4dg1OwzkC02Ipyy26GH3tIxZe1VSv5A==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 12, 12, 26, 39, 570, DateTimeKind.Local).AddTicks(7333));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "4d97e536-59a9-4c7c-9f74-40158f0ee93e");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2078660d-4e56-4e6d-924b-db3d6e17758b", "AQAAAAEAACcQAAAAEN2vmZqsFTvFQ3hwt1P2SvdFdtgQN2lIZKlfED0jXWFq2Pf6vH4LxQ/FkjIpFGO+TA==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 12, 12, 21, 16, 854, DateTimeKind.Local).AddTicks(5777));
        }
    }
}
