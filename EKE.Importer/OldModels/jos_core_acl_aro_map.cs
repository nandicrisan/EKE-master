namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_core_acl_aro_map")]
    public partial class jos_core_acl_aro_map
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int acl_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(230)]
        public string section_value { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string value { get; set; }
    }
}
