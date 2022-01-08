using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RSSolution.Data.Migrations
{
    public partial class user18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "ace3b6af-85f1-49fb-8e47-2f0f464512b2");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "28f936cf-0dca-44a8-ab80-8f4169fce1c1", "AQAAAAEAACcQAAAAEC3e4Wuf2ouPV3+wAPQdGIwoWuWmSIjk0ynQd7CzWZ7I/omBSimpzy0MkVpoaM+x0Q==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 7, 21, 17, 30, 294, DateTimeKind.Local).AddTicks(5416));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "919b425a-9f93-403b-8ce7-7a34084c1597");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "df84e763-090a-44c1-ba0c-5e741500fef8", "AQAAAAEAACcQAAAAEP3NUVCAr98Fufyq1Igyfk8JM5ggBzJQjxV3NgQTOxM8mhHOVXqArXJw5QGFTmA0pg==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 7, 12, 30, 7, 623, DateTimeKind.Local).AddTicks(8514));
        }
    }
}
