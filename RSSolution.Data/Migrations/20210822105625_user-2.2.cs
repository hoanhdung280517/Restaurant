using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RSSolution.Data.Migrations
{
    public partial class user22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookTables_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_BookTables_UserId",
                table: "BookTables",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookTables");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "6d47200e-7ae8-440d-8535-f9f0b52e00fe");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "79a223c5-f581-44d1-95cf-222fb50e84a0", "AQAAAAEAACcQAAAAEAcp21B3LK879DqlFcRBlcDUyRNvtP4ui+5VRM+IoGzrSQWxJDlAJiRj5nFywIkWlw==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 8, 19, 20, 4, 1, 411, DateTimeKind.Local).AddTicks(5824));
        }
    }
}
