namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_migration_backlinks")]
    public partial class jos_migration_backlinks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int itemid { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string url { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string sefurl { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string newurl { get; set; }
    }
}
