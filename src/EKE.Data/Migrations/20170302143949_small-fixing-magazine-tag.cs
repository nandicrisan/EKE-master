using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EKE.Data.Migrations
{
    public partial class smallfixingmagazinetag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MagazinTag");

            migrationBuilder.CreateTable(
                name: "MagazineTag",
                columns: table => new
                {
                    MagazinId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagazineTag", x => new { x.MagazinId, x.TagId });
                    table.ForeignKey(
                        name: "FK_MagazineTag_Magazines_MagazinId",
                        column: x => x.MagazinId,
                        principalTable: "Magazines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MagazineTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MagazineTag_TagId",
                table: "MagazineTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MagazineTag");

            migrationBuilder.CreateTable(
                name: "MagazinTag",
                columns: table => new
                {
                    MagazinId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagazinTag", x => new { x.MagazinId, x.TagId });
                    table.ForeignKey(
                        name: "FK_MagazinTag_Magazines_MagazinId",
                        column: x => x.MagazinId,
                        principalTable: "Magazines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MagazinTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MagazinTag_TagId",
                table: "MagazinTag",
                column: "TagId");
        }
    }
}
