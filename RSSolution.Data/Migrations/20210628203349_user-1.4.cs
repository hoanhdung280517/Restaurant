using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RSSolution.Data.Migrations
{
    public partial class user14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f517e2cb-998b-4123-8b0c-d171ef30ccaa");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dd9f9be7-b0ce-4ea3-bbcb-384efd6b3ea3", "AQAAAAEAACcQAAAAEJFVaj8CoWJdpCn/z42+CfwdkkZYIXEqmr/PKBxOh53mNFXQv3+/aentXGR3n9weug==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 6, 29, 3, 33, 48, 953, DateTimeKind.Local).AddTicks(7956));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "60fadbc8-f8a6-4b69-b05c-90aa6643e755");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b3412fc8-d7aa-4db3-a6c6-0244e523b87e", "AQAAAAEAACcQAAAAEHq68UCejQA8LGQiqAlfoiNgQ95LoAWJUVj89Dki54FCYo+iPV+zzFtjMbeqCATJ8g==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 6, 28, 22, 39, 13, 268, DateTimeKind.Local).AddTicks(8746));
        }
    }
}
