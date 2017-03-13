namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_xmap_ext")]
    public partial class jos_xmap_ext
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string extension { get; set; }

        public int? published { get; set; }

        [Column("params", TypeName = "text")]
        [StringLength(65535)]
        public string name_params { get; set; }
    }
}
