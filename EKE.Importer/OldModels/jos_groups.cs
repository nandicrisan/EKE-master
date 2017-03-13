namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_groups")]
    public partial class jos_groups
    {
        public byte id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }
    }
}
