namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_joomlawatch_internal")]
    public partial class jos_joomlawatch_internal
    {
        public int id { get; set; }

        [StringLength(255)]
        public string from { get; set; }

        [StringLength(255)]
        public string to { get; set; }

        public int? timestamp { get; set; }
    }
}
