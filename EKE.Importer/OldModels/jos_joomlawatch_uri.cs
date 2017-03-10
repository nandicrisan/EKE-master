namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_joomlawatch_uri")]
    public partial class jos_joomlawatch_uri
    {
        public int id { get; set; }

        public int? fk { get; set; }

        public int? timestamp { get; set; }

        [StringLength(255)]
        public string uri { get; set; }

        [StringLength(255)]
        public string title { get; set; }
    }
}
