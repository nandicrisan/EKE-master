namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_core_log_searches")]
    public partial class jos_core_log_searches
    {
        [Key]
        [Column(Order = 0)]
        public string search_term { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "uint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long hits { get; set; }
    }
}
