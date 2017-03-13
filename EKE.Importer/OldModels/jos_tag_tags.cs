namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_tag_tags")]
    public partial class jos_tag_tags
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string tagname { get; set; }

        public int parenttagid { get; set; }

        [Required]
        [StringLength(255)]
        public string component { get; set; }

        public int weight { get; set; }

        public int count { get; set; }

        public int countpublished { get; set; }

        public int access { get; set; }

        public int hits { get; set; }

        [Required]
        [StringLength(255)]
        public string template { get; set; }

        [Required]
        [StringLength(255)]
        public string output { get; set; }

        [Required]
        [StringLength(255)]
        public string sef { get; set; }

        [Required]
        [StringLength(16777215)]
        public string tagtext { get; set; }

        public int published { get; set; }

        public DateTime checked_out_time { get; set; }

        public int checked_out { get; set; }

        [Required]
        [StringLength(16777215)]
        public string desc { get; set; }

        [Required]
        [StringLength(255)]
        public string meta_title { get; set; }

        [Required]
        [StringLength(16777215)]
        public string meta_desc { get; set; }

        [Required]
        [StringLength(16777215)]
        public string meta_keywords { get; set; }

        [Required]
        [StringLength(10)]
        public string layout_dir { get; set; }

        [Required]
        [StringLength(255)]
        public string layout_orderby { get; set; }

        public DateTime created { get; set; }

        public int created_by { get; set; }
    }
}
