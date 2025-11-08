using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZivotopisCore.Migrations
{
    /// <inheritdoc />
    public partial class PridatArchivovanyStlpec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archivovany",
                table: "Pacienti",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archivovany",
                table: "Pacienti");
        }
    }
}
