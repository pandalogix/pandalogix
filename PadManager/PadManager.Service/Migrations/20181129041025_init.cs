﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PadManager.Service.Migrations
{
    public partial class init : Migration
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

            migrationBuilder.CreateTable(
                name: "Pads",
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
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CurrentMaxSequenceId = table.Column<int>(nullable: false),
                    TriggerData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstanceMapping",
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
                    PadId = table.Column<long>(nullable: true),
                    FieldMappings = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstanceMapping_Pads_PadId",
                        column: x => x.PadId,
                        principalTable: "Pads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Node",
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
                    PadId = table.Column<long>(nullable: true),
                    NodeId = table.Column<int>(nullable: false),
                    InNodes = table.Column<string>(nullable: true),
                    OutNodes = table.Column<string>(nullable: true),
                    MetaData = table.Column<string>(nullable: true),
                    NodeType = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Node", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Node_Pads_PadId",
                        column: x => x.PadId,
                        principalTable: "Pads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountPad_PadId",
                table: "AccountPad",
                column: "PadId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountPad_UserId",
                table: "AccountPad",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceMapping_Identifier",
                table: "InstanceMapping",
                column: "Identifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstanceMapping_PadId",
                table: "InstanceMapping",
                column: "PadId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_Identifier",
                table: "Node",
                column: "Identifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Node_PadId",
                table: "Node",
                column: "PadId");

            migrationBuilder.CreateIndex(
                name: "IX_PadExecutionHistory_PadIdentifier",
                table: "PadExecutionHistory",
                column: "PadIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_PadExecutionHistory_UserId",
                table: "PadExecutionHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pads_Identifier",
                table: "Pads",
                column: "Identifier",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountPad");

            migrationBuilder.DropTable(
                name: "InstanceMapping");

            migrationBuilder.DropTable(
                name: "Node");

            migrationBuilder.DropTable(
                name: "PadExecutionHistory");

            migrationBuilder.DropTable(
                name: "Pads");
        }
    }
}
