using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EKE.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "MediaElements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaElements_ArticleId",
                table: "MediaElements",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaElements_Articles_ArticleId",
                table: "MediaElements",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaElements_Articles_ArticleId",
                table: "MediaElements");

            migrationBuilder.DropIndex(
                name: "IX_MediaElements_ArticleId",
                table: "MediaElements");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "MediaElements");
        }
    }
}
