﻿// <auto-generated />
using EKE.Data;
using EKE.Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EKE.Data.Migrations
{
    [DbContext(typeof(BaseDbContext))]
    [Migration("20180223142436_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EKE.Data.Entities.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("EKE.Data.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AuthorId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateCreated");

                    b.Property<int?>("MagazineId");

                    b.Property<string>("OldContent");

                    b.Property<int>("OrderNo");

                    b.Property<string>("PublishedBy");

                    b.Property<bool>("Selected");

                    b.Property<string>("Slug")
                        .IsRequired();

                    b.Property<string>("Subtitle");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("MagazineId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.ArticleTag", b =>
                {
                    b.Property<int>("ArticleId");

                    b.Property<int>("TagId");

                    b.HasKey("ArticleId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ArticleTag");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.Magazine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AuthorId");

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("OrderNo");

                    b.Property<string>("PublishSection")
                        .IsRequired();

                    b.Property<int>("PublishYear");

                    b.Property<string>("PublishedBy");

                    b.Property<string>("Slug");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<bool>("Visible");

                    b.Property<string>("YumpuKey");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Magazines");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.MagazineCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PublishedBy");

                    b.HasKey("Id");

                    b.ToTable("MagazineCategories");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.MagazineTag", b =>
                {
                    b.Property<int>("MagazinId");

                    b.Property<int>("TagId");

                    b.HasKey("MagazinId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("MagazineTag");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.MediaElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ArticleId");

                    b.Property<int?>("AuthorId");

                    b.Property<string>("Description");

                    b.Property<int?>("ElementId");

                    b.Property<int?>("MagazineId");

                    b.Property<string>("Name");

                    b.Property<string>("OriginalName");

                    b.Property<int>("Scope");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ElementId");

                    b.HasIndex("MagazineId");

                    b.ToTable("MediaElements");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.Synonym", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Main");

                    b.Property<string>("Name");

                    b.Property<int?>("SynonymId");

                    b.HasKey("Id");

                    b.HasIndex("SynonymId");

                    b.ToTable("Synonyms");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("EKE.Data.Entities.Museum.Element", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DatePublished");

                    b.Property<string>("Description");

                    b.Property<string>("Publisher");

                    b.Property<bool>("Selected");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("M_Element");
                });

            modelBuilder.Entity("EKE.Data.Entities.Museum.ElementCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Name");

                    b.Property<int>("OrderNo");

                    b.Property<int?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("M_ElementCategory");
                });

            modelBuilder.Entity("EKE.Data.Entities.Museum.ElementTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<DateTime>("DateCreated");

                    b.Property<int?>("ElementId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ElementId");

                    b.ToTable("M_ElementTag");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.Article", b =>
                {
                    b.HasOne("EKE.Data.Entities.Gyopar.Author", "Author")
                        .WithMany("Articles")
                        .HasForeignKey("AuthorId");

                    b.HasOne("EKE.Data.Entities.Gyopar.Magazine", "Magazine")
                        .WithMany("Articles")
                        .HasForeignKey("MagazineId");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.ArticleTag", b =>
                {
                    b.HasOne("EKE.Data.Entities.Gyopar.Article", "Article")
                        .WithMany("ArticleTag")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EKE.Data.Entities.Gyopar.Tag", "Tag")
                        .WithMany("ArticleTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.Magazine", b =>
                {
                    b.HasOne("EKE.Data.Entities.Gyopar.Author", "Author")
                        .WithMany("Magazines")
                        .HasForeignKey("AuthorId");

                    b.HasOne("EKE.Data.Entities.Gyopar.MagazineCategory", "Category")
                        .WithMany("Magazines")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.MagazineTag", b =>
                {
                    b.HasOne("EKE.Data.Entities.Gyopar.Magazine", "Magazin")
                        .WithMany("MagazineTags")
                        .HasForeignKey("MagazinId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EKE.Data.Entities.Gyopar.Tag", "Tag")
                        .WithMany("MagazineTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.MediaElement", b =>
                {
                    b.HasOne("EKE.Data.Entities.Gyopar.Article")
                        .WithMany("MediaElement")
                        .HasForeignKey("ArticleId");

                    b.HasOne("EKE.Data.Entities.Gyopar.Author", "Author")
                        .WithMany("MediaElements")
                        .HasForeignKey("AuthorId");

                    b.HasOne("EKE.Data.Entities.Museum.Element")
                        .WithMany("MediaElement")
                        .HasForeignKey("ElementId");

                    b.HasOne("EKE.Data.Entities.Gyopar.Magazine")
                        .WithMany("MediaElements")
                        .HasForeignKey("MagazineId");
                });

            modelBuilder.Entity("EKE.Data.Entities.Gyopar.Synonym", b =>
                {
                    b.HasOne("EKE.Data.Entities.Gyopar.Synonym")
                        .WithMany("Synonyms")
                        .HasForeignKey("SynonymId");
                });

            modelBuilder.Entity("EKE.Data.Entities.Museum.Element", b =>
                {
                    b.HasOne("EKE.Data.Entities.Museum.ElementCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("EKE.Data.Entities.Museum.ElementCategory", b =>
                {
                    b.HasOne("EKE.Data.Entities.Museum.ElementCategory", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("EKE.Data.Entities.Museum.ElementTag", b =>
                {
                    b.HasOne("EKE.Data.Entities.Museum.Element")
                        .WithMany("Tags")
                        .HasForeignKey("ElementId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("EKE.Data.Entities.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EKE.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EKE.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("EKE.Data.Entities.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EKE.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EKE.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
