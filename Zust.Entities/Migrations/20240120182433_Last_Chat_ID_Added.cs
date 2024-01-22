using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zust.Entities.Migrations
{
    public partial class Last_Chat_ID_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastChatId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastChatId",
                table: "AspNetUsers");
        }
    }
}
