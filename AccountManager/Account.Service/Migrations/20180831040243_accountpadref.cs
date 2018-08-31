using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Account.Service.Migrations
{
    public partial class accountpadref : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountPad",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    PadId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPad", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountPad_PadId",
                table: "AccountPad",
                column: "PadId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountPad_UserId",
                table: "AccountPad",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountPad");
        }
    }
}
