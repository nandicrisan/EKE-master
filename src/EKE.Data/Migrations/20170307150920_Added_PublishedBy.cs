using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EKE.Data.Migrations
{
    public partial class Added_PublishedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublishedBy",
                table: "MagazineCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublishedBy",
                table: "Magazines",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublishedBy",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishedBy",
                table: "MagazineCategories");

            migrationBuilder.DropColumn(
                name: "PublishedBy",
                table: "Magazines");

            migrationBuilder.DropColumn(
                name: "PublishedBy",
                table: "Articles");
        }
    }
}
