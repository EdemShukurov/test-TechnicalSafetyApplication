using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalSafetyApplication.Migrations
{
    public partial class UpdateReply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Replies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Replies");
        }
    }
}
