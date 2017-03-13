namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_wf_profiles")]
    public partial class jos_wf_profiles
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string description { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string users { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string types { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string components { get; set; }

        public sbyte area { get; set; }

        [Required]
        [StringLength(255)]
        public string device { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string rows { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string plugins { get; set; }

        public sbyte published { get; set; }

        public int ordering { get; set; }

        public sbyte checked_out { get; set; }

        public DateTime checked_out_time { get; set; }

        [Column("params", TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string name_params { get; set; }
    }
}
