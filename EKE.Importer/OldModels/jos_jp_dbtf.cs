namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_jp_dbtf")]
    public partial class jos_jp_dbtf
    {
        [Key]
        public int dbtf_id { get; set; }

        [Required]
        [StringLength(16777215)]
        public string tablename { get; set; }
    }
}
