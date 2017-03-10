namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_jp_extradb")]
    public partial class jos_jp_extradb
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string host { get; set; }

        [Required]
        [StringLength(6)]
        public string port { get; set; }

        [Required]
        [StringLength(255)]
        public string username { get; set; }

        [Required]
        [StringLength(255)]
        public string password { get; set; }

        [Required]
        [StringLength(255)]
        public string database { get; set; }

        public sbyte usefilters { get; set; }

        public sbyte active { get; set; }
    }
}
