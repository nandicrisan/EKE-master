namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_jp_def")]
    public partial class jos_jp_def
    {
        [Key]
        [Column(TypeName = "uint")]
        public long def_id { get; set; }

        [Required]
        [StringLength(16777215)]
        public string directory { get; set; }
    }
}
