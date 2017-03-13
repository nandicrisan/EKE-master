namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_ak_stats")]
    public partial class jos_ak_stats
    {
        [Column(TypeName = "ubigint")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal id { get; set; }

        [Required]
        [StringLength(255)]
        public string description { get; set; }

        [StringLength(1073741823)]
        public string comment { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime backupstart { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime backupend { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string status { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string origin { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string type { get; set; }

        public long profile_id { get; set; }

        [StringLength(1073741823)]
        public string archivename { get; set; }

        [StringLength(1073741823)]
        public string absolute_path { get; set; }

        public int multipart { get; set; }
    }
}
