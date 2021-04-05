using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.SamuraiWhoSaidAWord
                    @text VARCHAR(20)
                    AS 
                    SELECT Samurais.Id, Samurais.Name,Samurais.ClanId
                    FROM Samurais INNER JOIN Quotes On Samurais.Id=Quotes.SamuraiId
                    WHERE (Quotes.Text LIKE'%'+@text+'%')");
            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.DeleteQuotesForSamurai
                   @samuraiId int 
                    AS 
                    DELETE FROM Quotes
                    WHERE Quotes.SamuraiId=@samuraiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP procedure dbo.SamuraiWhoSaidAWord");
            migrationBuilder.Sql("DROP procedure dbo.DeleteQuotesForSamurai");
        }
    }
}
