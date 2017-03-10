namespace EKE.Importer.NewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Magazine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Magazine()
        {
            Articles = new HashSet<Article>();
            MediaElements = new HashSet<MediaElement>();
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        public int? AuthorId { get; set; }

        public int? CategoryId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string PublishSection { get; set; }

        public int PublishYear { get; set; }

        public string PublishedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }

        public virtual Author Author { get; set; }

        public virtual MagazineCategory MagazineCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MediaElement> MediaElements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
