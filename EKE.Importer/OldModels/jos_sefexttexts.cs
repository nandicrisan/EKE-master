namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_sefexttexts")]
    public partial class jos_sefexttexts
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string extension { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(100)]
        public string value { get; set; }
    }
}
