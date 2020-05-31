using Microsoft.EntityFrameworkCore.Migrations;

namespace TechnicalSafetyApplication.Migrations
{
    public partial class RemoveForeignKeyReplyforApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Replies_ReplyId",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_ReplyId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Replies");

            migrationBuilder.AlterColumn<int>(
                name: "ReplyId",
                table: "Claims",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Replies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ReplyId",
                table: "Claims",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ReplyId",
                table: "Claims",
                column: "ReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Replies_ReplyId",
                table: "Claims",
                column: "ReplyId",
                principalTable: "Replies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
