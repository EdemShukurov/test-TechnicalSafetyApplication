using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalSafetyApplication.Migrations
{
    public partial class GetClaims_Procedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetApplicationsByUserId]
                    @userId nvarchar(450)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    select * from [dbo].[Claims] where [dbo].[Claims].[UserId] = @userId
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
