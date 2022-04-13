using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pickflicks2.Migrations
{
    public partial class movietable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StreamingService",
                table: "MWGInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MoviesInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MWGId = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieOverview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieReleaseYear = table.Column<int>(type: "int", nullable: false),
                    MovieIMDBRating = table.Column<int>(type: "int", nullable: false),
                    MovieImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviesInfo");

            migrationBuilder.DropColumn(
                name: "StreamingService",
                table: "MWGInfo");
        }
    }
}
