namespace EKE.Importer.OldModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wwwgyopar.jos_components")]
    public partial class jos_components
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string link { get; set; }

        [Column(TypeName = "uint")]
        public long menuid { get; set; }

        [Column(TypeName = "uint")]
        public long parent { get; set; }

        [Required]
        [StringLength(255)]
        public string admin_menu_link { get; set; }

        [Required]
        [StringLength(255)]
        public string admin_menu_alt { get; set; }

        [Required]
        [StringLength(50)]
        public string option { get; set; }

        public int ordering { get; set; }

        [Required]
        [StringLength(255)]
        public string admin_menu_img { get; set; }

        public sbyte iscore { get; set; }

        [Column("params", TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string name_params { get; set; }

        public sbyte enabled { get; set; }
    }
}
