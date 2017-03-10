namespace EKE.Importer.NewModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NewModels : DbContext
    {
        public NewModels()
            : base("name=NewModels1")
        {
        }

        public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<MagazineCategory> MagazineCategories { get; set; }
        public virtual DbSet<Magazine> Magazines { get; set; }
        public virtual DbSet<MediaElement> MediaElements { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Articles)
                .Map(m => m.ToTable("ArticleTag").MapLeftKey("ArticleId").MapRightKey("TagId"));

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetRoleClaims)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<MagazineCategory>()
                .HasMany(e => e.Magazines)
                .WithOptional(e => e.MagazineCategory)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Magazine>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Magazines)
                .Map(m => m.ToTable("MagazineTag").MapLeftKey("MagazinId").MapRightKey("TagId"));
        }
    }
}
