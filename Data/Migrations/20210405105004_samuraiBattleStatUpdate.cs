using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class samuraiBattleStatUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE OR ALTER VIEW dbo.SamuraiBattleStats
                AS
                SELECT
                    dbo.SamuraiBattle.SamuraiId,
                    dbo.Samurais.Name,
                    dbo.Battles.Name AS EarliestBattle,
                    COUNT(dbo.SamuraiBattle.BattleId) AS NumberofBattles
                FROM dbo.SamuraiBattle 
                     INNER JOIN dbo.Samurais on dbo.SamuraiBattle.SamuraiId=dbo.Samurais.Id
                     INNER JOIN dbo.Battles on dbo.SamuraiBattle.BattleId=dbo.Battles.Id
                GROUP By dbo.Samurais.Name,dbo.Battles.Name,dbo.SamuraiBattle.SamuraiId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE dbo.SamuraiBattleStats");
        }
    }
}
