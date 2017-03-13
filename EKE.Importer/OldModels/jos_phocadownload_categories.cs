namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_phocadownload_categories")]
    public partial class jos_phocadownload_categories
    {
        public int id { get; set; }

        public int parent_id { get; set; }

        public int section { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string alias { get; set; }

        [Required]
        [StringLength(255)]
        public string image { get; set; }

        [Required]
        [StringLength(30)]
        public string image_position { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string description { get; set; }

        public bool published { get; set; }

        [Column(TypeName = "uint")]
        public long checked_out { get; set; }

        public DateTime checked_out_time { get; set; }

        [StringLength(50)]
        public string editor { get; set; }

        public int ordering { get; set; }

        public byte access { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string uploaduserid { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string accessuserid { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string deleteuserid { get; set; }

        public DateTime date { get; set; }

        public int count { get; set; }

        [Column("params", TypeName = "text")]
        [StringLength(65535)]
        public string name_params { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string metakey { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string metadesc { get; set; }
    }
}
