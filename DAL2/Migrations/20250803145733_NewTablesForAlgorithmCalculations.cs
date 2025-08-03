using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewTablesForAlgorithmCalculations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceAccuracy",
                table: "tb_algorithm_accuracy");

            migrationBuilder.RenameColumn(
                name: "WinAccuracy",
                table: "tb_algorithm_accuracy",
                newName: "Accuracy");

            migrationBuilder.CreateTable(
                name: "tb_algorithm_event_accuracy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlgorithmId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EventEntityId = table.Column<int>(type: "int", nullable: false),
                    NumberOfPicks = table.Column<int>(type: "int", nullable: false),
                    NumberOfCorrectPicks = table.Column<int>(type: "int", nullable: false),
                    Accuracy = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_algorithm_event_accuracy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_event_accuracy_tb_algorithm_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "tb_algorithm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_event_accuracy_tb_event_EventEntityId",
                        column: x => x.EventEntityId,
                        principalTable: "tb_event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_algorithm_race_horse_total_score",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlgorithmId = table.Column<int>(type: "int", nullable: false),
                    RaceHorseId = table.Column<int>(type: "int", nullable: false),
                    TotalScore = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    RequiredOdds = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxPlace = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_algorithm_race_horse_total_score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_race_horse_total_score_tb_algorithm_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "tb_algorithm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_race_horse_total_score_tb_race_horse_RaceHorseId",
                        column: x => x.RaceHorseId,
                        principalTable: "tb_race_horse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_algorithm_race_variable_score",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlgorithmId = table.Column<int>(type: "int", nullable: false),
                    VariableId = table.Column<int>(type: "int", nullable: false),
                    RaceHorseId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_algorithm_race_variable_score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_race_variable_score_tb_algorithm_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "tb_algorithm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_race_variable_score_tb_race_horse_RaceHorseId",
                        column: x => x.RaceHorseId,
                        principalTable: "tb_race_horse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_race_variable_score_tb_variable_VariableId",
                        column: x => x.VariableId,
                        principalTable: "tb_variable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_algorithm_race_prediction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlgorithmId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EventEntityId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    PickOneId = table.Column<int>(type: "int", nullable: false),
                    PickTwoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_algorithm_race_prediction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_race_prediction_tb_algorithm_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "tb_algorithm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_race_prediction_tb_algorithm_race_horse_total_score_PickOneId",
                        column: x => x.PickOneId,
                        principalTable: "tb_algorithm_race_horse_total_score",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_race_prediction_tb_algorithm_race_horse_total_score_PickTwoId",
                        column: x => x.PickTwoId,
                        principalTable: "tb_algorithm_race_horse_total_score",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_race_prediction_tb_event_EventEntityId",
                        column: x => x.EventEntityId,
                        principalTable: "tb_event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_race_prediction_tb_race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "tb_race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_event_accuracy_AlgorithmId",
                table: "tb_algorithm_event_accuracy",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_event_accuracy_EventEntityId",
                table: "tb_algorithm_event_accuracy",
                column: "EventEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_race_horse_total_score_AlgorithmId",
                table: "tb_algorithm_race_horse_total_score",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_race_horse_total_score_RaceHorseId",
                table: "tb_algorithm_race_horse_total_score",
                column: "RaceHorseId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_race_prediction_AlgorithmId",
                table: "tb_algorithm_race_prediction",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_race_prediction_EventEntityId",
                table: "tb_algorithm_race_prediction",
                column: "EventEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_race_prediction_PickOneId",
                table: "tb_algorithm_race_prediction",
                column: "PickOneId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_race_prediction_PickTwoId",
                table: "tb_algorithm_race_prediction",
                column: "PickTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_race_prediction_RaceId",
                table: "tb_algorithm_race_prediction",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_race_variable_score_AlgorithmId",
                table: "tb_algorithm_race_variable_score",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_race_variable_score_RaceHorseId",
                table: "tb_algorithm_race_variable_score",
                column: "RaceHorseId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_race_variable_score_VariableId",
                table: "tb_algorithm_race_variable_score",
                column: "VariableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_algorithm_event_accuracy");

            migrationBuilder.DropTable(
                name: "tb_algorithm_race_prediction");

            migrationBuilder.DropTable(
                name: "tb_algorithm_race_variable_score");

            migrationBuilder.DropTable(
                name: "tb_algorithm_race_horse_total_score");

            migrationBuilder.RenameColumn(
                name: "Accuracy",
                table: "tb_algorithm_accuracy",
                newName: "WinAccuracy");

            migrationBuilder.AddColumn<decimal>(
                name: "PlaceAccuracy",
                table: "tb_algorithm_accuracy",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
