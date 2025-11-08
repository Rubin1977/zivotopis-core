using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZivotopisCore.Migrations
{
    /// <inheritdoc />
    public partial class AddDiagnozyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnozaModelPacientModel_DiagnozaModel_DiagnozyId",
                table: "DiagnozaModelPacientModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiagnozaModel",
                table: "DiagnozaModel");

            migrationBuilder.RenameTable(
                name: "DiagnozaModel",
                newName: "Diagnozy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diagnozy",
                table: "Diagnozy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnozaModelPacientModel_Diagnozy_DiagnozyId",
                table: "DiagnozaModelPacientModel",
                column: "DiagnozyId",
                principalTable: "Diagnozy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnozaModelPacientModel_Diagnozy_DiagnozyId",
                table: "DiagnozaModelPacientModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diagnozy",
                table: "Diagnozy");

            migrationBuilder.RenameTable(
                name: "Diagnozy",
                newName: "DiagnozaModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiagnozaModel",
                table: "DiagnozaModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnozaModelPacientModel_DiagnozaModel_DiagnozyId",
                table: "DiagnozaModelPacientModel",
                column: "DiagnozyId",
                principalTable: "DiagnozaModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
