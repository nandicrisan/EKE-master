using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EKE.Data.Migrations
{
    public partial class initialdatabasesetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Photographers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photographers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisterStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkShops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Slug = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShops_Photographers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkShops_BlogItems_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BlogItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Newsletters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    MagazineId = table.Column<int>(nullable: true),
                    Slug = table.Column<string>(nullable: false),
                    SubTitle = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Newsletters_Photographers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Newsletters_WorkShops_MagazineId",
                        column: x => x.MagazineId,
                        principalTable: "WorkShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                        name: "FK_MagazinTag_WorkShops_MagazinId",
                        column: x => x.MagazinId,
                        principalTable: "WorkShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MagazinTag_RegisterStatus_TagId",
                        column: x => x.TagId,
                        principalTable: "RegisterStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillingDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MagazineId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OriginalName = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingDatas_Photographers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillingDatas_WorkShops_MagazineId",
                        column: x => x.MagazineId,
                        principalTable: "WorkShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTag",
                columns: table => new
                {
                    ArticleId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTag", x => new { x.ArticleId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ArticleTag_Newsletters_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Newsletters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTag_RegisterStatus_TagId",
                        column: x => x.TagId,
                        principalTable: "RegisterStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Newsletters_AuthorId",
                table: "Newsletters",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Newsletters_MagazineId",
                table: "Newsletters",
                column: "MagazineId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTag_TagId",
                table: "ArticleTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShops_AuthorId",
                table: "WorkShops",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShops_CategoryId",
                table: "WorkShops",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MagazinTag_TagId",
                table: "MagazinTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingDatas_AuthorId",
                table: "BillingDatas",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingDatas_MagazineId",
                table: "BillingDatas",
                column: "MagazineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleTag");

            migrationBuilder.DropTable(
                name: "MagazinTag");

            migrationBuilder.DropTable(
                name: "BillingDatas");

            migrationBuilder.DropTable(
                name: "Newsletters");

            migrationBuilder.DropTable(
                name: "RegisterStatus");

            migrationBuilder.DropTable(
                name: "WorkShops");

            migrationBuilder.DropTable(
                name: "Photographers");

            migrationBuilder.DropTable(
                name: "BlogItems");
        }
    }
}
