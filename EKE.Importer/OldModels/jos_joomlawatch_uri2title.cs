namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_joomlawatch_uri2title")]
    public partial class jos_joomlawatch_uri2title
    {
        public int id { get; set; }

        [StringLength(255)]
        public string uri { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        public int? count { get; set; }

        public int? timestamp { get; set; }
    }
}
