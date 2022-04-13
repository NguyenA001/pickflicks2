using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pickflicks2.Migrations
{
    public partial class table9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChosenGenres",
                table: "MWGInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MembersNames",
                table: "MWGInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GenreRankingInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MWGId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Genre1 = table.Column<int>(type: "int", nullable: false),
                    Genre2 = table.Column<int>(type: "int", nullable: false),
                    Genre3 = table.Column<int>(type: "int", nullable: false),
                    Genre4 = table.Column<int>(type: "int", nullable: false),
                    Genre5 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreRankingInfo", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "GenreRankingInfo",
                columns: new[] { "Id", "Genre1", "Genre2", "Genre3", "Genre4", "Genre5", "MWGId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1, 2, 1, 1 },
                    { 2, 1, 1, 1, 1, 2, 1, 2 }
                });

            migrationBuilder.UpdateData(
                table: "MWGInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ChosenGenres", "MembersNames" },
                values: new object[] { "Drama,Thriller,Comedy,Romance,ScienceFiction", "Dylan,Sophie,An" });

            migrationBuilder.UpdateData(
                table: "MWGInfo",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ChosenGenres", "MembersNames" },
                values: new object[] { "Drama,Thriller,Comedy,Romance,ScienceFiction", "Dylan,Sophie,Angel" });

            migrationBuilder.UpdateData(
                table: "MWGInfo",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ChosenGenres", "MembersNames" },
                values: new object[] { "Drama,Thriller,Comedy,Romance,ScienceFiction", "Dylan,Sophie,An,JT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreRankingInfo");

            migrationBuilder.DropColumn(
                name: "ChosenGenres",
                table: "MWGInfo");

            migrationBuilder.DropColumn(
                name: "MembersNames",
                table: "MWGInfo");
        }
    }
}
