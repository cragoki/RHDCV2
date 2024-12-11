using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddingWinAndPlaceAccuracies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Accuracy",
                table: "tb_algorithm_execution",
                newName: "WinAccuracy");

            migrationBuilder.AddColumn<decimal>(
                name: "PlaceAccuracy",
                table: "tb_algorithm_execution",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceAccuracy",
                table: "tb_algorithm_execution");

            migrationBuilder.RenameColumn(
                name: "WinAccuracy",
                table: "tb_algorithm_execution",
                newName: "Accuracy");
        }
    }
}
