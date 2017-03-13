namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_sefwords")]
    public partial class jos_sefwords
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string word { get; set; }
    }
}
