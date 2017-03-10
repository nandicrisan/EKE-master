namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_messages_cfg")]
    public partial class jos_messages_cfg
    {
        [Key]
        [Column(Order = 0, TypeName = "uint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long user_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string cfg_name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string cfg_value { get; set; }
    }
}
