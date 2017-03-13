namespace EKE.Importer.NewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MediaElement
    {
        public int Id { get; set; }

        public Nullable<int> AuthorId { get; set; }

        public string Description { get; set; }

        public int? MagazineId { get; set; }

        public string Name { get; set; }

        public string OriginalName { get; set; }

        public int Type { get; set; }

        public int? ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public virtual Author Author { get; set; }

        public virtual Magazine Magazine { get; set; }
    }
}
