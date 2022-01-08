using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RSSolution.Data.Migrations
{
    public partial class user17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f53719c2-5499-488c-84a3-cabbb88c76dc");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a8b1aab7-5b42-43e9-950a-5227106c74d3", "AQAAAAEAACcQAAAAEDYtCJURDLe2NOKUNEsne8frCnhxBCHYUfKfeoGpgoQ+6KRyyL/ZQaW4M9L+PSB+SA==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 7, 12, 24, 56, 518, DateTimeKind.Local).AddTicks(3248));
        }
    }
}
