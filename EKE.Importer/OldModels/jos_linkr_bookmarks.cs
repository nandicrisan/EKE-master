namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_linkr_bookmarks")]
    public partial class jos_linkr_bookmarks
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string text { get; set; }

        [Required]
        [StringLength(20)]
        public string size { get; set; }

        [Column(TypeName = "tinytext")]
        [Required]
        [StringLength(255)]
        public string htmltext { get; set; }

        [Column(TypeName = "tinytext")]
        [Required]
        [StringLength(255)]
        public string htmlsmall { get; set; }

        [Column(TypeName = "tinytext")]
        [Required]
        [StringLength(255)]
        public string htmllarge { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string htmlbutton { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string htmlcustom { get; set; }

        public sbyte ordering { get; set; }

        [Required]
        [StringLength(100)]
        public string icon { get; set; }

        public int popular { get; set; }
    }
}
