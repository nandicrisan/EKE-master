namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_tag_layouts")]
    public partial class jos_tag_layouts
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [Required]
        [StringLength(255)]
        public string filename { get; set; }

        [Required]
        [StringLength(255)]
        public string appliesto { get; set; }

        public int framework { get; set; }

        [Required]
        [StringLength(16777215)]
        public string desc { get; set; }
    }
}
