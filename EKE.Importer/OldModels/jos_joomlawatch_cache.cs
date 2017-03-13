namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_joomlawatch_cache")]
    public partial class jos_joomlawatch_cache
    {
        public int id { get; set; }

        [StringLength(255)]
        public string key { get; set; }

        public int? lastUpdate { get; set; }

        [StringLength(16777215)]
        public string cache { get; set; }
    }
}
