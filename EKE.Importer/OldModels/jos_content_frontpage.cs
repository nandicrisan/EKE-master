namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_content_frontpage")]
    public partial class jos_content_frontpage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int content_id { get; set; }

        public int ordering { get; set; }
    }
}
