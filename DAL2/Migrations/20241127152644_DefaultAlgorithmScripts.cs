using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class DefaultAlgorithmScripts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO tb_algorithm VALUES
                (0, 1,1,1),
                (1, 0,0,1)

                DECLARE @AlphabeticalId INT  = (SELECT Id FROM tb_algorithm WHERE AlgorithmType = 0)
                DECLARE @BentersId INT  = (SELECT Id FROM tb_algorithm WHERE AlgorithmType = 1)


                INSERT INTO tb_algorithm_variable VALUES
                (@AlphabeticalId, 0, 3),
                (@BentersId, 0, 3)
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
