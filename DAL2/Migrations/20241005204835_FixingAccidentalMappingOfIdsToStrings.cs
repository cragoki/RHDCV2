using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixingAccidentalMappingOfIdsToStrings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_race_horse_tb_attire_category_AttireCategoryEntityId",
                table: "tb_race_horse");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_race_horse_tb_distance_between_category_DistanceBetweenCategoryEntityId",
                table: "tb_race_horse");

            migrationBuilder.DropIndex(
                name: "IX_tb_race_horse_AttireCategoryEntityId",
                table: "tb_race_horse");

            migrationBuilder.DropIndex(
                name: "IX_tb_race_horse_DistanceBetweenCategoryEntityId",
                table: "tb_race_horse");

            migrationBuilder.DropColumn(
                name: "AttireCategoryEntityId",
                table: "tb_race_horse");

            migrationBuilder.DropColumn(
                name: "DistanceBetweenCategoryEntityId",
                table: "tb_race_horse");

            migrationBuilder.AlterColumn<int>(
                name: "DistanceBetweenCategoryId",
                table: "tb_race_horse",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttireCategoryId",
                table: "tb_race_horse",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_race_horse_AttireCategoryId",
                table: "tb_race_horse",
                column: "AttireCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_race_horse_DistanceBetweenCategoryId",
                table: "tb_race_horse",
                column: "DistanceBetweenCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_race_horse_tb_attire_category_AttireCategoryId",
                table: "tb_race_horse",
                column: "AttireCategoryId",
                principalTable: "tb_attire_category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_race_horse_tb_distance_between_category_DistanceBetweenCategoryId",
                table: "tb_race_horse",
                column: "DistanceBetweenCategoryId",
                principalTable: "tb_distance_between_category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_race_horse_tb_attire_category_AttireCategoryId",
                table: "tb_race_horse");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_race_horse_tb_distance_between_category_DistanceBetweenCategoryId",
                table: "tb_race_horse");

            migrationBuilder.DropIndex(
                name: "IX_tb_race_horse_AttireCategoryId",
                table: "tb_race_horse");

            migrationBuilder.DropIndex(
                name: "IX_tb_race_horse_DistanceBetweenCategoryId",
                table: "tb_race_horse");

            migrationBuilder.AlterColumn<string>(
                name: "DistanceBetweenCategoryId",
                table: "tb_race_horse",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AttireCategoryId",
                table: "tb_race_horse",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttireCategoryEntityId",
                table: "tb_race_horse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DistanceBetweenCategoryEntityId",
                table: "tb_race_horse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tb_race_horse_AttireCategoryEntityId",
                table: "tb_race_horse",
                column: "AttireCategoryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_race_horse_DistanceBetweenCategoryEntityId",
                table: "tb_race_horse",
                column: "DistanceBetweenCategoryEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_race_horse_tb_attire_category_AttireCategoryEntityId",
                table: "tb_race_horse",
                column: "AttireCategoryEntityId",
                principalTable: "tb_attire_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_race_horse_tb_distance_between_category_DistanceBetweenCategoryEntityId",
                table: "tb_race_horse",
                column: "DistanceBetweenCategoryEntityId",
                principalTable: "tb_distance_between_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
