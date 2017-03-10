namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_newsfeeds")]
    public partial class jos_newsfeeds
    {
        public int catid { get; set; }

        public int id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string alias { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string link { get; set; }

        [StringLength(200)]
        public string filename { get; set; }

        public bool published { get; set; }

        [Column(TypeName = "uint")]
        public long numarticles { get; set; }

        [Column(TypeName = "uint")]
        public long cache_time { get; set; }

        public byte checked_out { get; set; }

        public DateTime checked_out_time { get; set; }

        public int ordering { get; set; }

        public sbyte rtl { get; set; }
    }
}
