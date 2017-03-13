namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_tag_hideshow")]
    public partial class jos_tag_hideshow
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string scope { get; set; }

        public int idnumber { get; set; }

        public int hideshow { get; set; }

        [Required]
        [StringLength(255)]
        public string spare { get; set; }
    }
}
