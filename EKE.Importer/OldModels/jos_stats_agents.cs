namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_stats_agents")]
    public partial class jos_stats_agents
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string agent { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool type { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "uint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long hits { get; set; }
    }
}
