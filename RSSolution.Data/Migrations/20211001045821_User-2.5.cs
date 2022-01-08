using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RSSolution.Data.Migrations
{
    public partial class User25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mealId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Meals_mealId",
                        column: x => x.mealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "27a93df7-438f-4bdb-8ec6-c614ab5f29b9");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d335aa74-7a5a-4116-9564-993f10f4b666", "AQAAAAEAACcQAAAAEPcsqs6FFSeGwCcME4axQHglhT0Cet3wHNAxfd9KqcQBKdSWLODt/SvkrmiGWmROlg==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 10, 1, 11, 58, 20, 839, DateTimeKind.Local).AddTicks(7523));

            migrationBuilder.CreateIndex(
                name: "IX_Comment_mealId",
                table: "Comment",
                column: "mealId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

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
    }
}
