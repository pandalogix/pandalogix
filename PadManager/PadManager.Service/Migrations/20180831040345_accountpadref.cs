using Microsoft.EntityFrameworkCore.Migrations;

namespace PadManager.Service.Migrations
{
    public partial class accountpadref : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TriggerData",
                table: "Pads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TriggerData",
                table: "Pads");
        }
    }
}
