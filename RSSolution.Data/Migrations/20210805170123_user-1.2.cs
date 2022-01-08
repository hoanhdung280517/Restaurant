using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RSSolution.Data.Migrations
{
    public partial class user12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "OrderDetails");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "039b6544-4bd0-482f-b2b6-a5b7ed6e2b0a");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5c9ad01b-0e52-42b2-8dba-df1ec9634536", "AQAAAAEAACcQAAAAEKls9JafTDHI35G7E6eIkismO+zmDtGU7p2COacRkaTBs/kFNvD/L3B30ONXCVy7AA==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 27, 23, 14, 56, 298, DateTimeKind.Local).AddTicks(3733));
        }
    }
}
