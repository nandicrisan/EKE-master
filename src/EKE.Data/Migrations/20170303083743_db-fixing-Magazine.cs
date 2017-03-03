using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EKE.Data.Migrations
{
    public partial class dbfixingMagazine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Magazines");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "Magazines",
                newName: "DateCreated");

            migrationBuilder.AddColumn<int>(
                name: "PublishSection",
                table: "Magazines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PublishYear",
                table: "Magazines",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishSection",
                table: "Magazines");

            migrationBuilder.DropColumn(
                name: "PublishYear",
                table: "Magazines");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Magazines",
                newName: "DateAdded");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Magazines",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
