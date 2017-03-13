namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_templates_menu")]
    public partial class jos_templates_menu
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string template { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int menuid { get; set; }

        [Key]
        [Column(Order = 2)]
        public sbyte client_id { get; set; }
    }
}
