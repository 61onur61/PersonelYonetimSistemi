using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonelYonetimSistemi.Data.Migrations
{
    public partial class ResimPathEklendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResimPath",
                table: "PersonelIsEmirleris",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResimPath",
                table: "PersonelIsEmirleris");
        }
    }
}
