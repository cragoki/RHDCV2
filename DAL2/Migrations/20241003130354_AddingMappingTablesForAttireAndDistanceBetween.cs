using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddingMappingTablesForAttireAndDistanceBetween : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistanceBetween",
                table: "tb_race_horse",
                newName: "DistanceBetweenCategoryId");

            migrationBuilder.AddColumn<int>(
                name: "AttireCategoryEntityId",
                table: "tb_race_horse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AttireCategoryId",
                table: "tb_race_horse",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistanceBetweenCategoryEntityId",
                table: "tb_race_horse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tb_attire_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_attire_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_distance_between_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_distance_between_category", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_race_horse_tb_attire_category_AttireCategoryEntityId",
                table: "tb_race_horse");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_race_horse_tb_distance_between_category_DistanceBetweenCategoryEntityId",
                table: "tb_race_horse");

            migrationBuilder.DropTable(
                name: "tb_attire_category");

            migrationBuilder.DropTable(
                name: "tb_distance_between_category");

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
                name: "AttireCategoryId",
                table: "tb_race_horse");

            migrationBuilder.DropColumn(
                name: "DistanceBetweenCategoryEntityId",
                table: "tb_race_horse");

            migrationBuilder.RenameColumn(
                name: "DistanceBetweenCategoryId",
                table: "tb_race_horse",
                newName: "DistanceBetween");
        }
    }
}
