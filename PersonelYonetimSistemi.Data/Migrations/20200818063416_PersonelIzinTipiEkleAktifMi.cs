using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonelYonetimSistemi.Data.Migrations
{
    public partial class PersonelIzinTipiEkleAktifMi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AktifMi",
                table: "personelIzinTipis",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AktifMi",
                table: "personelIzinTipis");
        }
    }
}
