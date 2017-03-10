namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_phocadownload_settings")]
    public partial class jos_phocadownload_settings
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        [Required]
        [StringLength(250)]
        public string title { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string value { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string values { get; set; }

        [Required]
        [StringLength(50)]
        public string type { get; set; }
    }
}
