using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EKE.Data.Migrations
{
    public partial class Museum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElementId",
                table: "MediaElements",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "M_ElementCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_ElementCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "M_Element",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DatePublished = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Publisher = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_Element", x => x.Id);
                    table.ForeignKey(
                        name: "FK_M_Element_M_ElementCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "M_ElementCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaElements_ElementId",
                table: "MediaElements",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_M_Element_CategoryId",
                table: "M_Element",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaElements_M_Element_ElementId",
                table: "MediaElements",
                column: "ElementId",
                principalTable: "M_Element",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaElements_M_Element_ElementId",
                table: "MediaElements");

            migrationBuilder.DropTable(
                name: "M_Element");

            migrationBuilder.DropTable(
                name: "M_ElementCategory");

            migrationBuilder.DropIndex(
                name: "IX_MediaElements_ElementId",
                table: "MediaElements");

            migrationBuilder.DropColumn(
                name: "ElementId",
                table: "MediaElements");
        }
    }
}
