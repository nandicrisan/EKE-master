namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_core_acl_aro_groups")]
    public partial class jos_core_acl_aro_groups
    {
        public int id { get; set; }

        public int parent_id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        public int lft { get; set; }

        public int rgt { get; set; }

        [Required]
        [StringLength(255)]
        public string value { get; set; }
    }
}
