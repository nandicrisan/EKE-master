namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_xmap_items")]
    public partial class jos_xmap_items
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string uid { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int itemid { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string view { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sitemap_id { get; set; }

        [StringLength(300)]
        public string properties { get; set; }
    }
}
