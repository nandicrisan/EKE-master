namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_weblinks")]
    public partial class jos_weblinks
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        public int catid { get; set; }

        public int sid { get; set; }

        [Required]
        [StringLength(250)]
        public string title { get; set; }

        [Required]
        [StringLength(255)]
        public string alias { get; set; }

        [Required]
        [StringLength(250)]
        public string url { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string description { get; set; }

        public DateTime date { get; set; }

        public int hits { get; set; }

        public bool published { get; set; }

        public int checked_out { get; set; }

        public DateTime checked_out_time { get; set; }

        public int ordering { get; set; }

        public bool archived { get; set; }

        public bool approved { get; set; }

        [Column("params", TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string name_params { get; set; }
    }
}
