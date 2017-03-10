namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_joomlawatch_cc2c")]
    public partial class jos_joomlawatch_cc2c
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte id { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string cc { get; set; }

        [Required]
        [StringLength(50)]
        public string country { get; set; }
    }
}
