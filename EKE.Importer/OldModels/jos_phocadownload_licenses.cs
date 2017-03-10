namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_phocadownload_licenses")]
    public partial class jos_phocadownload_licenses
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [Required]
        [StringLength(255)]
        public string alias { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string description { get; set; }

        [Column(TypeName = "uint")]
        public long checked_out { get; set; }

        public DateTime checked_out_time { get; set; }

        public bool published { get; set; }

        public int ordering { get; set; }
    }
}
