namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_poll_data")]
    public partial class jos_poll_data
    {
        public int id { get; set; }

        public int pollid { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string text { get; set; }

        public int hits { get; set; }
    }
}
