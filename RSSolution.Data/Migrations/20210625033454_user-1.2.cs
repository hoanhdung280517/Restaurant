using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RSSolution.Data.Migrations
{
    public partial class user12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "a3fdd4d8-04f4-4d26-b832-5cba98d8b5ba");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "02c8678e-cc64-48cf-9336-f908841f7905", "AQAAAAEAACcQAAAAEPqFF0Mm3fx3nTekF1j7QUZiiDqVAGwZH1cjn22vjadXOi6wsJpWAIzyVQjc+2Jitw==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 6, 25, 10, 34, 53, 404, DateTimeKind.Local).AddTicks(9732));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "cfca0ba7-8231-496c-b931-a8e2ff056d93");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f771298c-2a16-481c-8fc3-22fb15392c02", "AQAAAAEAACcQAAAAELBOV7jhCsuie8/AePivWlnLj9JLITQgDW4R2gdMp6qua0q3Fkq4G3x6xs6UkYi3OA==" });

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 6, 25, 0, 2, 41, 731, DateTimeKind.Local).AddTicks(3499));
        }
    }
}
