namespace EKE.Importer.NewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            MediaElements = new HashSet<MediaElement>();
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        public Nullable<int> AuthorId { get; set; }

        public string Content { get; set; }

        public string OldContent { get; set; }

        public int? MagazineId { get; set; }

        [Required]
        public string Slug { get; set; }

        public string Subtitle { get; set; }

        [Required]
        public string Title { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateCreated { get; set; }

        public string PublishedBy { get; set; }

        public int OrderNo { get; set; }

        public virtual Author Author { get; set; }

        public virtual Magazine Magazine { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MediaElement> MediaElements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
