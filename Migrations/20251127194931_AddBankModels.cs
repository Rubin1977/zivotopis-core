using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZivotopisCore.Migrations
{
    /// <inheritdoc />
    public partial class AddBankModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produkty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nazov = table.Column<string>(type: "text", nullable: false),
                    Popis = table.Column<string>(type: "text", nullable: false),
                    Cena = table.Column<decimal>(type: "numeric", nullable: false),
                    DatumPridania = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Aktivny = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ucty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CisloUctu = table.Column<string>(type: "text", nullable: false),
                    Majitel = table.Column<string>(type: "text", nullable: false),
                    Zostatok = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transakcie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Datum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Suma = table.Column<decimal>(type: "numeric", nullable: false),
                    Protiucet = table.Column<string>(type: "text", nullable: false),
                    Popis = table.Column<string>(type: "text", nullable: false),
                    Archivovana = table.Column<bool>(type: "boolean", nullable: false),
                    UcetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transakcie_Ucty_UcetId",
                        column: x => x.UcetId,
                        principalTable: "Ucty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transakcie_UcetId",
                table: "Transakcie",
                column: "UcetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produkty");

            migrationBuilder.DropTable(
                name: "Transakcie");

            migrationBuilder.DropTable(
                name: "Ucty");
        }
    }
}
