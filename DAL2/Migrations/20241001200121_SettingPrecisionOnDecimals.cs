using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SettingPrecisionOnDecimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_event_RaceCourse_RaceCourseId",
                table: "tb_event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RaceCourse",
                table: "RaceCourse");

            migrationBuilder.RenameTable(
                name: "RaceCourse",
                newName: "tb_race_course");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "tb_race_horse",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "RaceUrl",
                table: "tb_race",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_race_course",
                table: "tb_race_course",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_event_tb_race_course_RaceCourseId",
                table: "tb_event",
                column: "RaceCourseId",
                principalTable: "tb_race_course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_event_tb_race_course_RaceCourseId",
                table: "tb_event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_race_course",
                table: "tb_race_course");

            migrationBuilder.DropColumn(
                name: "RaceUrl",
                table: "tb_race");

            migrationBuilder.RenameTable(
                name: "tb_race_course",
                newName: "RaceCourse");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "tb_race_horse",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RaceCourse",
                table: "RaceCourse",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_event_RaceCourse_RaceCourseId",
                table: "tb_event",
                column: "RaceCourseId",
                principalTable: "RaceCourse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
