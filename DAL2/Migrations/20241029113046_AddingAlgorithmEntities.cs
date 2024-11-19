using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddingAlgorithmEntities : Migration
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
                    AlgorithmType = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_algorithm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_algorithm_execution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlgorithmId = table.Column<int>(type: "int", nullable: false),
                    Accuracy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfRaces = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_algorithm_execution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_execution_tb_algorithm_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "tb_algorithm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_algorithm_variable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlgorithmId = table.Column<int>(type: "int", nullable: false),
                    IncludeAllWeather = table.Column<bool>(type: "bit", nullable: false),
                    ClassLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_algorithm_variable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_algorithm_variable_tb_algorithm_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "tb_algorithm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_execution_AlgorithmId",
                table: "tb_algorithm_execution",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_algorithm_variable_AlgorithmId",
                table: "tb_algorithm_variable",
                column: "AlgorithmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_algorithm_execution");

            migrationBuilder.DropTable(
                name: "tb_algorithm_variable");

            migrationBuilder.DropTable(
                name: "tb_algorithm");
        }
    }
}
