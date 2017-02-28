using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EKE.Data.Entities;

namespace EKE.Data
{
    public class BaseDbContext : IdentityDbContext<ApplicationUser>
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        #region Entities
        DbSet<Article> Newsletters { get; set; }
        DbSet<Author> Photographers { get; set; }
        DbSet<Magazine> WorkShops { get; set; }
        DbSet<MagazineCategory> BlogItems { get; set; }
        DbSet<MediaElement> BillingDatas { get; set; }
        DbSet<Tag> RegisterStatus { get; set; }
        #endregion

        //Model configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Magazin to tag relationship
            modelBuilder.Entity<MagazinTag>()
           .HasKey(t => new { t.MagazinId, t.TagId });

            modelBuilder.Entity<MagazinTag>()
                .HasOne(pt => pt.Magazin)
                .WithMany(p => p.MagazinTags)
                .HasForeignKey(pt => pt.MagazinId);

            modelBuilder.Entity<MagazinTag>()
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
