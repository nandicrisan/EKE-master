namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_bannerclient")]
    public partial class jos_bannerclient
    {
        [Key]
        public int cid { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string contact { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string extrainfo { get; set; }

        public bool checked_out { get; set; }

        public TimeSpan? checked_out_time { get; set; }

        [StringLength(50)]
        public string editor { get; set; }
    }
}
