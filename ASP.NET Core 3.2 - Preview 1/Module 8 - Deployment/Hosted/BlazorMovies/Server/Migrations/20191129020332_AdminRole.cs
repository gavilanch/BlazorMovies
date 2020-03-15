using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorMovies.Server.Migrations
{
    public partial class AdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO AspNetRoles (Id, [Name], NormalizedName)
VALUES('2a4707ac-34be-4293-b565-29297b216dd5', 'Admin', 'Admin')
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
