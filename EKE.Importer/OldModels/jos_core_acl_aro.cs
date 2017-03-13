namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_core_acl_aro")]
    public partial class jos_core_acl_aro
    {
        public int id { get; set; }

        [Required]
        [StringLength(240)]
        public string section_value { get; set; }

        [Required]
        [StringLength(240)]
        public string value { get; set; }

        public int order_value { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        public int hidden { get; set; }
    }
}
