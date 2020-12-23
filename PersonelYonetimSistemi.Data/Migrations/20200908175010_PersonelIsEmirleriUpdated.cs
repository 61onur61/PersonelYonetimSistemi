using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonelYonetimSistemi.Data.Migrations
{
    public partial class PersonelIsEmirleriUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonelIsEmirleris_AspNetUsers_PersonelIsAtamaId1",
                table: "PersonelIsEmirleris");

            migrationBuilder.DropIndex(
                name: "IX_PersonelIsEmirleris_PersonelIsAtamaId1",
                table: "PersonelIsEmirleris");

            migrationBuilder.DropColumn(
                name: "PersonelIsAtamaId1",
                table: "PersonelIsEmirleris");

            migrationBuilder.AlterColumn<string>(
                name: "PersonelIsAtamaId",
                table: "PersonelIsEmirleris",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IsEmriNumara",
                table: "PersonelIsEmirleris",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DegistirilmeTarihi",
                table: "PersonelIsEmirleris",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelIsEmirleris_PersonelIsAtamaId",
                table: "PersonelIsEmirleris",
                column: "PersonelIsAtamaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonelIsEmirleris_AspNetUsers_PersonelIsAtamaId",
                table: "PersonelIsEmirleris",
                column: "PersonelIsAtamaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonelIsEmirleris_AspNetUsers_PersonelIsAtamaId",
                table: "PersonelIsEmirleris");

            migrationBuilder.DropIndex(
                name: "IX_PersonelIsEmirleris_PersonelIsAtamaId",
                table: "PersonelIsEmirleris");

            migrationBuilder.AlterColumn<int>(
                name: "PersonelIsAtamaId",
                table: "PersonelIsEmirleris",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsEmriNumara",
                table: "PersonelIsEmirleris",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DegistirilmeTarihi",
                table: "PersonelIsEmirleris",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonelIsAtamaId1",
                table: "PersonelIsEmirleris",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonelIsEmirleris_PersonelIsAtamaId1",
                table: "PersonelIsEmirleris",
                column: "PersonelIsAtamaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonelIsEmirleris_AspNetUsers_PersonelIsAtamaId1",
                table: "PersonelIsEmirleris",
                column: "PersonelIsAtamaId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
