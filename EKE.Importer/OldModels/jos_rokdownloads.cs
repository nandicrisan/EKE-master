namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_rokdownloads")]
    public partial class jos_rokdownloads
    {
        public int id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string name { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string displayname { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string path { get; set; }

        public sbyte folder { get; set; }

        public long filesize { get; set; }

        [StringLength(16777215)]
        public string introtext { get; set; }

        [StringLength(16777215)]
        public string fulltext { get; set; }

        [StringLength(255)]
        public string thumbnail { get; set; }

        public int access { get; set; }

        [Column("params", TypeName = "text")]
        [StringLength(65535)]
        public string name_params { get; set; }

        public int downloads { get; set; }

        public sbyte published { get; set; }

        [Column(TypeName = "uint")]
        public long lft { get; set; }

        [Column(TypeName = "uint")]
        public long rgt { get; set; }

        public DateTime created_time { get; set; }

        public int created_by { get; set; }

        public DateTime modified_time { get; set; }

        public int modified_by { get; set; }

        public DateTime checked_out_time { get; set; }

        public int checked_out { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string metadata { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string metadesc { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string metakey { get; set; }
    }
}
