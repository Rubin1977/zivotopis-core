using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZivotopisCore.Migrations
{
    /// <inheritdoc />
    public partial class AddPriezviskoToPacient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Priezvisko",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priezvisko",
                table: "Pacienti");
        }
    }
}
