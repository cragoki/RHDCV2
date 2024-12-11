using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewAlgorithmVariablesForBentersModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AgeOfHorseEnabled",
                table: "tb_algorithm_variable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CurrentConditionEnabled",
                table: "tb_algorithm_variable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CurrentConditionPoints",
                table: "tb_algorithm_variable",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PerformanceInLastXRacesEnabled",
                table: "tb_algorithm_variable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PerformanceInLastXRacesTake",
                table: "tb_algorithm_variable",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TimeSinceLastRaceEnabled",
                table: "tb_algorithm_variable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeOfHorseEnabled",
                table: "tb_algorithm_variable");

            migrationBuilder.DropColumn(
                name: "CurrentConditionEnabled",
                table: "tb_algorithm_variable");

            migrationBuilder.DropColumn(
                name: "CurrentConditionPoints",
                table: "tb_algorithm_variable");

            migrationBuilder.DropColumn(
                name: "PerformanceInLastXRacesEnabled",
                table: "tb_algorithm_variable");

            migrationBuilder.DropColumn(
                name: "PerformanceInLastXRacesTake",
                table: "tb_algorithm_variable");

            migrationBuilder.DropColumn(
                name: "TimeSinceLastRaceEnabled",
                table: "tb_algorithm_variable");
        }
    }
}
