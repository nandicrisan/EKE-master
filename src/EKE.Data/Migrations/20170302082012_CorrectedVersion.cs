using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EKE.Data.Migrations
{
    public partial class CorrectedVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Newsletters_Photographers_AuthorId",
                table: "Newsletters");

            migrationBuilder.DropForeignKey(
                name: "FK_Newsletters_WorkShops_MagazineId",
                table: "Newsletters");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTag_Newsletters_ArticleId",
                table: "ArticleTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTag_RegisterStatus_TagId",
                table: "ArticleTag");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkShops_Photographers_AuthorId",
                table: "WorkShops");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkShops_BlogItems_CategoryId",
                table: "WorkShops");

            migrationBuilder.DropForeignKey(
                name: "FK_MagazinTag_WorkShops_MagazinId",
                table: "MagazinTag");

            migrationBuilder.DropForeignKey(
                name: "FK_MagazinTag_RegisterStatus_TagId",
                table: "MagazinTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BillingDatas_Photographers_AuthorId",
                table: "BillingDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_BillingDatas_WorkShops_MagazineId",
                table: "BillingDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisterStatus",
                table: "RegisterStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillingDatas",
                table: "BillingDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogItems",
                table: "BlogItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkShops",
                table: "WorkShops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photographers",
                table: "Photographers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Newsletters",
                table: "Newsletters");

            migrationBuilder.RenameTable(
                name: "RegisterStatus",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "BillingDatas",
                newName: "MediaElements");

            migrationBuilder.RenameTable(
                name: "BlogItems",
                newName: "MagazineCategories");

            migrationBuilder.RenameTable(
                name: "WorkShops",
                newName: "Magazines");

            migrationBuilder.RenameTable(
                name: "Photographers",
                newName: "Authors");

            migrationBuilder.RenameTable(
                name: "Newsletters",
                newName: "Articles");

            migrationBuilder.RenameIndex(
                name: "IX_BillingDatas_MagazineId",
                table: "MediaElements",
                newName: "IX_MediaElements_MagazineId");

            migrationBuilder.RenameIndex(
                name: "IX_BillingDatas_AuthorId",
                table: "MediaElements",
                newName: "IX_MediaElements_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkShops_CategoryId",
                table: "Magazines",
                newName: "IX_Magazines_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkShops_AuthorId",
                table: "Magazines",
                newName: "IX_Magazines_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Newsletters_MagazineId",
                table: "Articles",
                newName: "IX_Articles_MagazineId");

            migrationBuilder.RenameIndex(
                name: "IX_Newsletters_AuthorId",
                table: "Articles",
                newName: "IX_Articles_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaElements",
                table: "MediaElements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MagazineCategories",
                table: "MagazineCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Magazines",
                table: "Magazines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articles",
                table: "Articles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Authors_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Magazines_MagazineId",
                table: "Articles",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTag_Articles_ArticleId",
                table: "ArticleTag",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTag_Tags_TagId",
                table: "ArticleTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Magazines_Authors_AuthorId",
                table: "Magazines",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Magazines_MagazineCategories_CategoryId",
                table: "Magazines",
                column: "CategoryId",
                principalTable: "MagazineCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MagazinTag_Magazines_MagazinId",
                table: "MagazinTag",
                column: "MagazinId",
                principalTable: "Magazines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MagazinTag_Tags_TagId",
                table: "MagazinTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaElements_Authors_AuthorId",
                table: "MediaElements",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaElements_Magazines_MagazineId",
                table: "MediaElements",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Authors_AuthorId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Magazines_MagazineId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTag_Articles_ArticleId",
                table: "ArticleTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTag_Tags_TagId",
                table: "ArticleTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Magazines_Authors_AuthorId",
                table: "Magazines");

            migrationBuilder.DropForeignKey(
                name: "FK_Magazines_MagazineCategories_CategoryId",
                table: "Magazines");

            migrationBuilder.DropForeignKey(
                name: "FK_MagazinTag_Magazines_MagazinId",
                table: "MagazinTag");

            migrationBuilder.DropForeignKey(
                name: "FK_MagazinTag_Tags_TagId",
                table: "MagazinTag");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaElements_Authors_AuthorId",
                table: "MediaElements");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaElements_Magazines_MagazineId",
                table: "MediaElements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaElements",
                table: "MediaElements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MagazineCategories",
                table: "MagazineCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Magazines",
                table: "Magazines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articles",
                table: "Articles");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "RegisterStatus");

            migrationBuilder.RenameTable(
                name: "MediaElements",
                newName: "BillingDatas");

            migrationBuilder.RenameTable(
                name: "MagazineCategories",
                newName: "BlogItems");

            migrationBuilder.RenameTable(
                name: "Magazines",
                newName: "WorkShops");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Photographers");

            migrationBuilder.RenameTable(
                name: "Articles",
                newName: "Newsletters");

            migrationBuilder.RenameIndex(
                name: "IX_MediaElements_MagazineId",
                table: "BillingDatas",
                newName: "IX_BillingDatas_MagazineId");

            migrationBuilder.RenameIndex(
                name: "IX_MediaElements_AuthorId",
                table: "BillingDatas",
                newName: "IX_BillingDatas_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Magazines_CategoryId",
                table: "WorkShops",
                newName: "IX_WorkShops_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Magazines_AuthorId",
                table: "WorkShops",
                newName: "IX_WorkShops_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_MagazineId",
                table: "Newsletters",
                newName: "IX_Newsletters_MagazineId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AuthorId",
                table: "Newsletters",
                newName: "IX_Newsletters_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisterStatus",
                table: "RegisterStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillingDatas",
                table: "BillingDatas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogItems",
                table: "BlogItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkShops",
                table: "WorkShops",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photographers",
                table: "Photographers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Newsletters",
                table: "Newsletters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Newsletters_Photographers_AuthorId",
                table: "Newsletters",
                column: "AuthorId",
                principalTable: "Photographers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Newsletters_WorkShops_MagazineId",
                table: "Newsletters",
                column: "MagazineId",
                principalTable: "WorkShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTag_Newsletters_ArticleId",
                table: "ArticleTag",
                column: "ArticleId",
                principalTable: "Newsletters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTag_RegisterStatus_TagId",
                table: "ArticleTag",
                column: "TagId",
                principalTable: "RegisterStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkShops_Photographers_AuthorId",
                table: "WorkShops",
                column: "AuthorId",
                principalTable: "Photographers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkShops_BlogItems_CategoryId",
                table: "WorkShops",
                column: "CategoryId",
                principalTable: "BlogItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MagazinTag_WorkShops_MagazinId",
                table: "MagazinTag",
                column: "MagazinId",
                principalTable: "WorkShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MagazinTag_RegisterStatus_TagId",
                table: "MagazinTag",
                column: "TagId",
                principalTable: "RegisterStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillingDatas_Photographers_AuthorId",
                table: "BillingDatas",
                column: "AuthorId",
                principalTable: "Photographers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillingDatas_WorkShops_MagazineId",
                table: "BillingDatas",
                column: "MagazineId",
                principalTable: "WorkShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
