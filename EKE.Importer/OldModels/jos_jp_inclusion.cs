namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_jp_inclusion")]
    public partial class jos_jp_inclusion
    {
        public long id { get; set; }

        [Column(TypeName = "uint")]
        public long profile { get; set; }

        [Column("class")]
        [Required]
        [StringLength(255)]
        public string name_class { get; set; }

        [Required]
        [StringLength(1073741823)]
        public string value { get; set; }
    }
}
