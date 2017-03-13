namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_tag_category_map")]
    public partial class jos_tag_category_map
    {
        public int id { get; set; }

        public int contentid { get; set; }

        public int tagid { get; set; }

        [Required]
        [StringLength(255)]
        public string scope { get; set; }

        public DateTime created { get; set; }

        public int created_by { get; set; }

        public DateTime checked_out_time { get; set; }

        public int checked_out { get; set; }
    }
}
