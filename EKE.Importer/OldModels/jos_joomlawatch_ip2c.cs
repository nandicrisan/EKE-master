namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_joomlawatch_ip2c")]
    public partial class jos_joomlawatch_ip2c
    {
        [Key]
        [Column(Order = 0, TypeName = "uint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long start { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "uint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long end { get; set; }

        [Column(TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string country { get; set; }
    }
}
