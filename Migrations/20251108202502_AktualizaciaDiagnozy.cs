using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZivotopisCore.Migrations
{
    /// <inheritdoc />
    public partial class AktualizaciaDiagnozy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aktivna",
                table: "Diagnozy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DátumVytvorenia",
                table: "Diagnozy",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Kod",
                table: "Diagnozy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Typ",
                table: "Diagnozy",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aktivna",
                table: "Diagnozy");

            migrationBuilder.DropColumn(
                name: "DátumVytvorenia",
                table: "Diagnozy");

            migrationBuilder.DropColumn(
                name: "Kod",
                table: "Diagnozy");

            migrationBuilder.DropColumn(
                name: "Typ",
                table: "Diagnozy");
        }
    }
}
