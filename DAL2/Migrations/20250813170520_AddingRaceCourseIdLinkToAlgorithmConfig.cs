using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddingRaceCourseIdLinkToAlgorithmConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RaceCourseId",
                table: "tb_algorithm_configuration",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_configuration_RaceCourseId",
                table: "tb_algorithm_configuration",
                column: "RaceCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_algorithm_configuration_tb_race_course_RaceCourseId",
                table: "tb_algorithm_configuration",
                column: "RaceCourseId",
                principalTable: "tb_race_course",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_algorithm_configuration_tb_race_course_RaceCourseId",
                table: "tb_algorithm_configuration");

            migrationBuilder.DropIndex(
                name: "IX_tb_algorithm_configuration_RaceCourseId",
                table: "tb_algorithm_configuration");

            migrationBuilder.DropColumn(
                name: "RaceCourseId",
                table: "tb_algorithm_configuration");
        }
    }
}
