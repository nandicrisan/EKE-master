namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_sefmoved")]
    public partial class jos_sefmoved
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string old { get; set; }

        [Column("new")]
        [Required]
        [StringLength(255)]
        public string name_new { get; set; }

        public DateTime lastHit { get; set; }
    }
}
