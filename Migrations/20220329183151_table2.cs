using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pickflicks2.Migrations
{
    public partial class table2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MWGInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "MembersId",
                value: "1,2,3");

            migrationBuilder.UpdateData(
                table: "MWGInfo",
                keyColumn: "Id",
                keyValue: 2,
                column: "MembersId",
                value: "1,2,4");

            migrationBuilder.UpdateData(
                table: "MWGInfo",
                keyColumn: "Id",
                keyValue: 3,
                column: "MembersId",
                value: "1,2,3,5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MWGInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "MembersId",
                value: "2,3");

            migrationBuilder.UpdateData(
                table: "MWGInfo",
                keyColumn: "Id",
                keyValue: 2,
                column: "MembersId",
                value: "1,4");

            migrationBuilder.UpdateData(
                table: "MWGInfo",
                keyColumn: "Id",
                keyValue: 3,
                column: "MembersId",
                value: "1,2,5");
        }
    }
}
