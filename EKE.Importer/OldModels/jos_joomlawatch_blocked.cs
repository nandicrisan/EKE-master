namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_joomlawatch_blocked")]
    public partial class jos_joomlawatch_blocked
    {
        public int id { get; set; }

        [StringLength(255)]
        public string ip { get; set; }

        public int? hits { get; set; }

        public int? date { get; set; }

        [StringLength(255)]
        public string reason { get; set; }
    }
}
