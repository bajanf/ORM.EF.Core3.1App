using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class SamuraiBattleStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE FUNCTION[dbo].[EarliestBattleFoughtBySamurai](@samuraiId int)
                RETURNS char(30) AS
                BEGIN
                DECLARE @ret char(30)
                SELECT TOP 1 @ret = Name 
                FROM Battles
                WHERE Battles.Id IN (SELECT BattleId FROM SamuraiBattle WHERE SamuraiId=@samuraiId)
                ORDER BY StartDate
                RETURN @ret
                END");
            migrationBuilder.Sql(
                @"CREATE OR ALTER VIEW dbo.SamuraiBattleStats
                AS
                SELECT
                    dbo.SamuraiBattle.SamuraiId,
                    dbo.Samurais.Name,
                    COUNT(dbo.SamuraiBattle.BattleId) AS NumberofBattles
                FROM dbo.SamuraiBattle 
                     INNER JOIN dbo.Samurais on dbo.SamuraiBattle.SamuraiId=dbo.Samurais.Id
                GROUP By dbo.Samurais.Name,dbo.SamuraiBattle.SamuraiId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE dbo.SamuraiBattleStats");
            migrationBuilder.Sql("DROP FUNCTION dbo.EarliestBattleFoughtBySamurai");
        }
    }
}
