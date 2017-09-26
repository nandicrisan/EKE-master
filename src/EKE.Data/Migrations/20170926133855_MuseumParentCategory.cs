using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EKE.Data.Migrations
{
    public partial class MuseumParentCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "M_ElementCategory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_M_ElementCategory_ParentId",
                table: "M_ElementCategory",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_M_ElementCategory_M_ElementCategory_ParentId",
                table: "M_ElementCategory",
                column: "ParentId",
                principalTable: "M_ElementCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_ElementCategory_M_ElementCategory_ParentId",
                table: "M_ElementCategory");

            migrationBuilder.DropIndex(
                name: "IX_M_ElementCategory_ParentId",
                table: "M_ElementCategory");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "M_ElementCategory");
        }
    }
}
