using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZivotopisCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diagnozy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Kod = table.Column<string>(type: "text", nullable: false),
                    Nazov = table.Column<string>(type: "text", nullable: false),
                    Popis = table.Column<string>(type: "text", nullable: true),
                    Typ = table.Column<string>(type: "text", nullable: true),
                    Aktivna = table.Column<bool>(type: "boolean", nullable: false),
                    DátumVytvorenia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnozy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacienti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Meno = table.Column<string>(type: "text", nullable: false),
                    Priezvisko = table.Column<string>(type: "text", nullable: false),
                    DatumNarodenia = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Pohlavie = table.Column<string>(type: "text", nullable: false),
                    RodneCislo = table.Column<string>(type: "text", nullable: false),
                    Archivovany = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacienti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiagnozaModelPacientModel",
                columns: table => new
                {
                    DiagnozyId = table.Column<int>(type: "integer", nullable: false),
                    PacientiId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnozaModelPacientModel", x => new { x.DiagnozyId, x.PacientiId });
                    table.ForeignKey(
                        name: "FK_DiagnozaModelPacientModel_Diagnozy_DiagnozyId",
                        column: x => x.DiagnozyId,
                        principalTable: "Diagnozy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnozaModelPacientModel_Pacienti_PacientiId",
                        column: x => x.PacientiId,
                        principalTable: "Pacienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Priznaky",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nazov = table.Column<string>(type: "text", nullable: false),
                    DatumZaznamenania = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PacientModelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priznaky", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Priznaky_Pacienti_PacientModelId",
                        column: x => x.PacientModelId,
                        principalTable: "Pacienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vysetrenia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nazov = table.Column<string>(type: "text", nullable: false),
                    Vysledok = table.Column<string>(type: "text", nullable: false),
                    DatumVysetrenia = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PacientModelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vysetrenia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vysetrenia_Pacienti_PacientModelId",
                        column: x => x.PacientModelId,
                        principalTable: "Pacienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnozaModelPacientModel_PacientiId",
                table: "DiagnozaModelPacientModel",
                column: "PacientiId");

            migrationBuilder.CreateIndex(
                name: "IX_Priznaky_PacientModelId",
                table: "Priznaky",
                column: "PacientModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Vysetrenia_PacientModelId",
                table: "Vysetrenia",
                column: "PacientModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagnozaModelPacientModel");

            migrationBuilder.DropTable(
                name: "Priznaky");

            migrationBuilder.DropTable(
                name: "Vysetrenia");

            migrationBuilder.DropTable(
                name: "Diagnozy");

            migrationBuilder.DropTable(
                name: "Pacienti");
        }
    }
}
