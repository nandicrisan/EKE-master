namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_jp_sff")]
    public partial class jos_jp_sff
    {
        [Key]
        [Column(TypeName = "uint")]
        public long sff_id { get; set; }

        [Required]
        [StringLength(16777215)]
        public string file { get; set; }
    }
}
