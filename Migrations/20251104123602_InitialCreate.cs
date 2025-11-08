using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZivotopisCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiagnozaModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Popis = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnozaModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacienti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Meno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumNarodenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pohlavie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RodneCislo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacienti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiagnozaModelPacientModel",
                columns: table => new
                {
                    DiagnozyId = table.Column<int>(type: "int", nullable: false),
                    PacientiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnozaModelPacientModel", x => new { x.DiagnozyId, x.PacientiId });
                    table.ForeignKey(
                        name: "FK_DiagnozaModelPacientModel_DiagnozaModel_DiagnozyId",
                        column: x => x.DiagnozyId,
                        principalTable: "DiagnozaModel",
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumZaznamenania = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PacientModelId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vysledok = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumVysetrenia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PacientModelId = table.Column<int>(type: "int", nullable: false)
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
                name: "DiagnozaModel");

            migrationBuilder.DropTable(
                name: "Pacienti");
        }
    }
}
