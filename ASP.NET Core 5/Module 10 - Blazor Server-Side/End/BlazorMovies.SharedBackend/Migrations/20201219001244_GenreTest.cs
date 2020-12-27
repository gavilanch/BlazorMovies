using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorMovies.SharedBackend.Migrations
{
    public partial class GenreTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Drama" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
