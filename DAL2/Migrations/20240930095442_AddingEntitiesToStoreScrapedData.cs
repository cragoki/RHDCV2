using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddingEntitiesToStoreScrapedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RaceCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SurfaceType = table.Column<int>(type: "int", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: true),
                    SpeedType = table.Column<int>(type: "int", nullable: true),
                    IsAllWeather = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_age_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_age_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_distance_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_distance_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_going_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_going_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_horse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_horse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_jockey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_jockey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_trainer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_trainer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceCourseId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_event_RaceCourse_RaceCourseId",
                        column: x => x.RaceCourseId,
                        principalTable: "RaceCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_race",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EventEntityId = table.Column<int>(type: "int", nullable: false),
                    Class = table.Column<int>(type: "int", nullable: false),
                    RaceType = table.Column<int>(type: "int", nullable: false),
                    AgeCategoryId = table.Column<int>(type: "int", nullable: false),
                    GoingCategoryId = table.Column<int>(type: "int", nullable: false),
                    DistanceCategoryId = table.Column<int>(type: "int", nullable: false),
                    RaceDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_race", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_race_tb_age_category_AgeCategoryId",
                        column: x => x.AgeCategoryId,
                        principalTable: "tb_age_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_race_tb_distance_category_DistanceCategoryId",
                        column: x => x.DistanceCategoryId,
                        principalTable: "tb_distance_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_race_tb_event_EventEntityId",
                        column: x => x.EventEntityId,
                        principalTable: "tb_event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_race_tb_going_category_GoingCategoryId",
                        column: x => x.GoingCategoryId,
                        principalTable: "tb_going_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_race_horse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    HorseId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    JockeyId = table.Column<int>(type: "int", nullable: false),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    Odds = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DistanceBetween = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_race_horse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_race_horse_tb_horse_HorseId",
                        column: x => x.HorseId,
                        principalTable: "tb_horse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_race_horse_tb_jockey_JockeyId",
                        column: x => x.JockeyId,
                        principalTable: "tb_jockey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_race_horse_tb_race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "tb_race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_race_horse_tb_trainer_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "tb_trainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_event_RaceCourseId",
                table: "tb_event",
                column: "RaceCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_race_AgeCategoryId",
                table: "tb_race",
                column: "AgeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_race_DistanceCategoryId",
                table: "tb_race",
                column: "DistanceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_race_EventEntityId",
                table: "tb_race",
                column: "EventEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_race_GoingCategoryId",
                table: "tb_race",
                column: "GoingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_race_horse_HorseId",
                table: "tb_race_horse",
                column: "HorseId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_race_horse_JockeyId",
                table: "tb_race_horse",
                column: "JockeyId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_race_horse_RaceId",
                table: "tb_race_horse",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_race_horse_TrainerId",
                table: "tb_race_horse",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_race_horse");

            migrationBuilder.DropTable(
                name: "tb_horse");

            migrationBuilder.DropTable(
                name: "tb_jockey");

            migrationBuilder.DropTable(
                name: "tb_race");

            migrationBuilder.DropTable(
                name: "tb_trainer");

            migrationBuilder.DropTable(
                name: "tb_age_category");

            migrationBuilder.DropTable(
                name: "tb_distance_category");

            migrationBuilder.DropTable(
                name: "tb_event");

            migrationBuilder.DropTable(
                name: "tb_going_category");

            migrationBuilder.DropTable(
                name: "RaceCourse");
        }
    }
}
