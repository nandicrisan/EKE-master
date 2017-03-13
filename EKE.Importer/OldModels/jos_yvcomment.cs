namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_yvcomment")]
    public partial class jos_yvcomment
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [Required]
        [StringLength(255)]
        public string alias { get; set; }

        [Required]
        [StringLength(255)]
        public string title_alias { get; set; }

        [Required]
        [StringLength(16777215)]
        public string introtext { get; set; }

        [Required]
        [StringLength(16777215)]
        public string fulltext { get; set; }

        public sbyte state { get; set; }

        [Column(TypeName = "uint")]
        public long sectionid { get; set; }

        [Column(TypeName = "uint")]
        public long mask { get; set; }

        [Column(TypeName = "uint")]
        public long catid { get; set; }

        public DateTime created { get; set; }

        [Column(TypeName = "uint")]
        public long created_by { get; set; }

        [Required]
        [StringLength(255)]
        public string created_by_alias { get; set; }

        public DateTime modified { get; set; }

        [Column(TypeName = "uint")]
        public long modified_by { get; set; }

        [Column(TypeName = "uint")]
        public long checked_out { get; set; }

        public DateTime checked_out_time { get; set; }

        public DateTime publish_up { get; set; }

        public DateTime publish_down { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string images { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string urls { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string attribs { get; set; }

        [Column(TypeName = "uint")]
        public long version { get; set; }

        [Column(TypeName = "uint")]
        public long parentid { get; set; }

        public int ordering { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string metakey { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string metadesc { get; set; }

        [Column(TypeName = "uint")]
        public long access { get; set; }

        [Column(TypeName = "uint")]
        public long hits { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string metadata { get; set; }
    }
}
