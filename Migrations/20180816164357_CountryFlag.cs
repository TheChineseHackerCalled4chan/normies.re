using Microsoft.EntityFrameworkCore.Migrations;

namespace NormiesRe.Migrations
{
    public partial class CountryFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Posts");
        }
    }
}
