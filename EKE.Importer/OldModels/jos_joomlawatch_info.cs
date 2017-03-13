namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_joomlawatch_info")]
    public partial class jos_joomlawatch_info
    {
        public int id { get; set; }

        public int? group { get; set; }

        public int? date { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public int? value { get; set; }
    }
}
