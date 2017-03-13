namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_sefexts")]
    public partial class jos_sefexts
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string file { get; set; }

        [Column("params", TypeName = "text")]
        [StringLength(65535)]
        public string name_params { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string filters { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }
    }
}
