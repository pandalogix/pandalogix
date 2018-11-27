using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PadManager.Service.Migrations
{
    public partial class addingexecutionhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Pads",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Node",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "InstanceMapping",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PadExecutionHistory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Identifier = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 255, nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    PadIdentifier = table.Column<Guid>(nullable: false),
                    ExecutionSummary = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PadExecutionHistory", x => x.Id);
                });


            migrationBuilder.CreateIndex(
                name: "IX_PadExecutionHistory_PadIdentifier",
                table: "PadExecutionHistory",
                column: "PadIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_PadExecutionHistory_UserId",
                table: "PadExecutionHistory",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PadExecutionHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pads");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InstanceMapping");
        }
    }
}
