namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_rokversions")]
    public partial class jos_rokversions
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string product { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string version { get; set; }
    }
}
