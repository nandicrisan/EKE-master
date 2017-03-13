namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_bfdbversions")]
    public partial class jos_bfdbversions
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string tablename { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string version { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string component { get; set; }
    }
}
