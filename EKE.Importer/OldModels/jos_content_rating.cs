namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_content_rating")]
    public partial class jos_content_rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int content_id { get; set; }

        [Column(TypeName = "uint")]
        public long rating_sum { get; set; }

        [Column(TypeName = "uint")]
        public long rating_count { get; set; }

        [Required]
        [StringLength(50)]
        public string lastip { get; set; }
    }
}
