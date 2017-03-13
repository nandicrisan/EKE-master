namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_phocadownload")]
    public partial class jos_phocadownload
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        public int catid { get; set; }

        public int sectionid { get; set; }

        public int owner_id { get; set; }

        public int sid { get; set; }

        [Required]
        [StringLength(250)]
        public string title { get; set; }

        [Required]
        [StringLength(255)]
        public string alias { get; set; }

        [Required]
        [StringLength(250)]
        public string filename { get; set; }

        public int filesize { get; set; }

        [Required]
        [StringLength(250)]
        public string filename_play { get; set; }

        [Required]
        [StringLength(250)]
        public string filename_preview { get; set; }

        [Required]
        [StringLength(255)]
        public string author { get; set; }

        [Required]
        [StringLength(255)]
        public string author_email { get; set; }

        [Required]
        [StringLength(255)]
        public string author_url { get; set; }

        [Required]
        [StringLength(255)]
        public string license { get; set; }

        [Required]
        [StringLength(255)]
        public string license_url { get; set; }

        [Required]
        [StringLength(255)]
        public string image_filename { get; set; }

        [Required]
        [StringLength(255)]
        public string image_filename_spec1 { get; set; }

        [Required]
        [StringLength(255)]
        public string image_filename_spec2 { get; set; }

        [Required]
        [StringLength(255)]
        public string image_download { get; set; }

        [Required]
        [StringLength(255)]
        public string link_external { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string description { get; set; }

        [Required]
        [StringLength(255)]
        public string version { get; set; }

        public bool directlink { get; set; }

        public DateTime date { get; set; }

        public DateTime publish_up { get; set; }

        public DateTime publish_down { get; set; }

        public int hits { get; set; }

        public bool textonly { get; set; }

        public bool published { get; set; }

        public byte approved { get; set; }

        public int checked_out { get; set; }

        public DateTime checked_out_time { get; set; }

        public int ordering { get; set; }

        public byte access { get; set; }

        public int confirm_license { get; set; }

        public int unaccessible_file { get; set; }

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
