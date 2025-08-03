using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewAlgorithmVariables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_algorithm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_algorithm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_algorithm_accuracy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlgorithmId = table.Column<int>(type: "int", nullable: false),
                    PlaceAccuracy = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    WinAccuracy = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_algorithm_accuracy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_accuracy_tb_algorithm_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "tb_algorithm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_algorithm_configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlgorithmId = table.Column<int>(type: "int", nullable: false),
                    VariableId = table.Column<int>(type: "int", nullable: false),
                    Importance = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_algorithm_configuration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_configuration_tb_algorithm_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "tb_algorithm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_configuration_tb_variable_VariableId",
                        column: x => x.VariableId,
                        principalTable: "tb_variable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_algorithm_result",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlgorithmId = table.Column<int>(type: "int", nullable: false),
                    VariableId = table.Column<int>(type: "int", nullable: false),
                    RaceHorseId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    AdjustedScore = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_algorithm_result", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_result_tb_algorithm_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "tb_algorithm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_result_tb_race_horse_RaceHorseId",
                        column: x => x.RaceHorseId,
                        principalTable: "tb_race_horse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_result_tb_variable_VariableId",
                        column: x => x.VariableId,
                        principalTable: "tb_variable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_accuracy_AlgorithmId",
                table: "tb_algorithm_accuracy",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_configuration_AlgorithmId",
                table: "tb_algorithm_configuration",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_configuration_VariableId",
                table: "tb_algorithm_configuration",
                column: "VariableId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_result_AlgorithmId",
                table: "tb_algorithm_result",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_result_RaceHorseId",
                table: "tb_algorithm_result",
                column: "RaceHorseId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_result_VariableId",
                table: "tb_algorithm_result",
                column: "VariableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_algorithm_accuracy");

            migrationBuilder.DropTable(
                name: "tb_algorithm_configuration");

            migrationBuilder.DropTable(
                name: "tb_algorithm_result");

            migrationBuilder.DropTable(
                name: "tb_algorithm");
        }
    }
}
