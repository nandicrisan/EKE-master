using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EKE.Data.Entities;
using EKE.Data.Entities.Gyopar;

namespace EKE.Data
{
    public class BaseDbContext : IdentityDbContext<ApplicationUser>
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        #region Entities
        DbSet<Article> Articles { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Magazine> Magazines { get; set; }
        DbSet<MagazineCategory> MagazineCategories { get; set; }
        DbSet<MediaElement> MediaElements { get; set; }
        DbSet<Tag> Tags { get; set; }
        #endregion

        //Model configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Magazin to tag relationship
            modelBuilder.Entity<MagazineTag>()
           .HasKey(t => new { t.MagazinId, t.TagId });

            modelBuilder.Entity<MagazineTag>()
                .HasOne(pt => pt.Magazin)
                .WithMany(p => p.MagazineTags)
                .HasForeignKey(pt => pt.MagazinId);

            modelBuilder.Entity<MagazineTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.MagazinTags)
                .HasForeignKey(pt => pt.TagId);

            //Article to tag relationship
            modelBuilder.Entity<ArticleTag>()
            .HasKey(t => new { t.ArticleId, t.TagId });

            modelBuilder.Entity<ArticleTag>()
                .HasOne(pt => pt.Article)
                .WithMany(p => p.ArticleTag)
                .HasForeignKey(pt => pt.ArticleId);

            modelBuilder.Entity<ArticleTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ArticleTags)
                .HasForeignKey(pt => pt.TagId);
            base.OnModelCreating(modelBuilder);
        }

        public virtual void Commit()
        {
            this.SaveChanges();
        }
    }
}
