namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_ak_profiles")]
    public partial class jos_ak_profiles
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string description { get; set; }

        [StringLength(1073741823)]
        public string configuration { get; set; }

        [StringLength(1073741823)]
        public string filters { get; set; }
    }
}
