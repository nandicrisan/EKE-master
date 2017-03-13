namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_xmap")]
    public partial class jos_xmap
    {
        [Key]
        [StringLength(30)]
        public string name { get; set; }

        [StringLength(100)]
        public string value { get; set; }
    }
}
