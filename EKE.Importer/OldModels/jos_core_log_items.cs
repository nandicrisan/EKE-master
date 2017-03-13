namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_core_log_items")]
    public partial class jos_core_log_items
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime time_stamp { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string item_table { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "uint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long item_id { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "uint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long hits { get; set; }
    }
}
