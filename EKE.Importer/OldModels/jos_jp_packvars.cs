namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_jp_packvars")]
    public partial class jos_jp_packvars
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string key { get; set; }

        [StringLength(255)]
        public string value { get; set; }

        [StringLength(1073741823)]
        public string value2 { get; set; }
    }
}
