using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonelYonetimSistemi.Data.Migrations
{
    public partial class PersonelTablolariEklendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "personelIzinTipis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(nullable: true),
                    GunSayisi = table.Column<int>(nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personelIzinTipis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonelIzinOnays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IzinBaslangicTarihi = table.Column<DateTime>(nullable: false),
                    IzinBitisTarihi = table.Column<DateTime>(nullable: false),
                    IzinBasvuruTarihi = table.Column<DateTime>(nullable: false),
                    IzinNotu = table.Column<string>(nullable: true),
                    OnaylandiMi = table.Column<bool>(nullable: true),
                    TamamlandiMi = table.Column<bool>(nullable: false),
                    PersonelTalepId = table.Column<string>(nullable: true),
                    PersonelOnayId = table.Column<string>(nullable: true),
                    PersonelIzinTipiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelIzinOnays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonelIzinOnays_personelIzinTipis_PersonelIzinTipiId",
                        column: x => x.PersonelIzinTipiId,
                        principalTable: "personelIzinTipis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelIzinOnays_AspNetUsers_PersonelOnayId",
                        column: x => x.PersonelOnayId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelIzinOnays_AspNetUsers_PersonelTalepId",
                        column: x => x.PersonelTalepId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonelIzins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GunSayisi = table.Column<int>(nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    PersonelId = table.Column<string>(nullable: true),
                    personelIzinTipiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelIzins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonelIzins_AspNetUsers_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelIzins_personelIzinTipis_personelIzinTipiId",
                        column: x => x.personelIzinTipiId,
                        principalTable: "personelIzinTipis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonelIzinOnays_PersonelIzinTipiId",
                table: "PersonelIzinOnays",
                column: "PersonelIzinTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelIzinOnays_PersonelOnayId",
                table: "PersonelIzinOnays",
                column: "PersonelOnayId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelIzinOnays_PersonelTalepId",
                table: "PersonelIzinOnays",
                column: "PersonelTalepId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelIzins_PersonelId",
                table: "PersonelIzins",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelIzins_personelIzinTipiId",
                table: "PersonelIzins",
                column: "personelIzinTipiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonelIzinOnays");

            migrationBuilder.DropTable(
                name: "PersonelIzins");

            migrationBuilder.DropTable(
                name: "personelIzinTipis");
        }
    }
}
