namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_menu_types")]
    public partial class jos_menu_types
    {
        [Column(TypeName = "uint")]
        public long id { get; set; }

        [Required]
        [StringLength(75)]
        public string menutype { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [Required]
        [StringLength(255)]
        public string description { get; set; }
    }
}
