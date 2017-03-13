namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_joomlawatch")]
    public partial class jos_joomlawatch
    {
        public int id { get; set; }

        [StringLength(255)]
        public string ip { get; set; }

        [StringLength(2)]
        public string country { get; set; }

        [StringLength(255)]
        public string browser { get; set; }

        [StringLength(255)]
        public string referer { get; set; }

        [StringLength(255)]
        public string username { get; set; }
    }
}
