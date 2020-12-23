using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonelYonetimSistemi.Data.Migrations
{
    public partial class IsEmirleriTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "PersonelIsEmirleris",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlusturulmaTarihi = table.Column<DateTime>(nullable: false),
                    DegistirilmeTarihi = table.Column<DateTime>(nullable: false),
                    IsEmriAciklamasi = table.Column<string>(nullable: true),
                    IsEmriDurumu = table.Column<int>(nullable: false),
                    IsEmriPuan = table.Column<double>(nullable: false),
                    IsEmriNumara = table.Column<string>(nullable: true),
                    PersonelIsAtamaId = table.Column<int>(nullable: false),
                    PersonelIsAtamaId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelIsEmirleris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonelIsEmirleris_AspNetUsers_PersonelIsAtamaId1",
                        column: x => x.PersonelIsAtamaId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonelIsEmirleris_PersonelIsAtamaId1",
                table: "PersonelIsEmirleris",
                column: "PersonelIsAtamaId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonelIsEmirleris");

            migrationBuilder.CreateTable(
                name: "IsEmirleris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DegistirilmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEmriAciklamasi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmriDurumu = table.Column<int>(type: "int", nullable: false),
                    IsEmriNumara = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmriPuan = table.Column<double>(type: "float", nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonelIsAtamaId = table.Column<int>(type: "int", nullable: false),
                    PersonelIsAtamaId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IsEmirleris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IsEmirleris_AspNetUsers_PersonelIsAtamaId1",
                        column: x => x.PersonelIsAtamaId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IsEmirleris_PersonelIsAtamaId1",
                table: "IsEmirleris",
                column: "PersonelIsAtamaId1");
        }
    }
}
