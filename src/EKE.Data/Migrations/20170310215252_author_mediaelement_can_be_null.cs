using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EKE.Data.Migrations
{
    public partial class author_mediaelement_can_be_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaElements_Authors_AuthorId",
                table: "MediaElements");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "MediaElements",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MediaElements_Authors_AuthorId",
                table: "MediaElements",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaElements_Authors_AuthorId",
                table: "MediaElements");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "MediaElements",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaElements_Authors_AuthorId",
                table: "MediaElements",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
