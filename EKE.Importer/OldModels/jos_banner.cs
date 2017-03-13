namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_banner")]
    public partial class jos_banner
    {
        [Key]
        public int bid { get; set; }

        public int cid { get; set; }

        [Required]
        [StringLength(30)]
        public string type { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string alias { get; set; }

        public int imptotal { get; set; }

        public int impmade { get; set; }

        public int clicks { get; set; }

        [Required]
        [StringLength(100)]
        public string imageurl { get; set; }

        [Required]
        [StringLength(200)]
        public string clickurl { get; set; }

        public DateTime? date { get; set; }

        public bool showBanner { get; set; }

        public bool checked_out { get; set; }

        public DateTime checked_out_time { get; set; }

        [StringLength(50)]
        public string editor { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string custombannercode { get; set; }

        [Column(TypeName = "uint")]
        public long catid { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string description { get; set; }

        public bool sticky { get; set; }

        public int ordering { get; set; }

        public DateTime publish_up { get; set; }

        public DateTime publish_down { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string tags { get; set; }

        [Column("params", TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string name_params { get; set; }
    }
}
