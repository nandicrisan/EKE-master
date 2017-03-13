namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_polls")]
    public partial class jos_polls
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [Required]
        [StringLength(255)]
        public string alias { get; set; }

        public int voters { get; set; }

        public int checked_out { get; set; }

        public DateTime checked_out_time { get; set; }

        public bool published { get; set; }

        public int access { get; set; }

        public int lag { get; set; }
    }
}
